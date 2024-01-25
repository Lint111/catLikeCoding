using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GPUGraph : MonoBehaviour
{
    const int maxResolution = 1000;

    [SerializeField,Range(10, maxResolution)]
    int resolution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    public enum TransitionMode { Cycle,Random}

    [SerializeField]
    TransitionMode transitionMode;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;

    [SerializeField]
    float highResulutionScale = 0.03f, lowResulutionScale = 3;

    [SerializeField]
    ComputeShader computeShader;

    [SerializeField]
    Material material;

    [SerializeField]
    Mesh mesh;

    static readonly int 
        positionsId = Shader.PropertyToID("_Positions"),
        resolutionId = Shader.PropertyToID("_Resolution"),
        stepId = Shader.PropertyToID("_Step"),
        timeId = Shader.PropertyToID("_Time"),
        transitionProgressId = Shader.PropertyToID("_TransitionProgress"),
        resolutionScaleId = Shader.PropertyToID("_ResolutionScale");

    float duration = 0;
    bool transitioning;
    FunctionLibrary.FunctionName transitionFunction;

    ComputeBuffer positionsBuffer;

    private void OnEnable()
    {
        positionsBuffer = new ComputeBuffer(maxResolution * maxResolution, 3 * 4);

    }
    private void OnDisable()
    {
        positionsBuffer.Dispose();
        positionsBuffer = null;
    }

    private void Update()
    {
        duration += Time.deltaTime;
        if(transitioning)
        {
            if (duration >= transitionDuration)
            {
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if(duration >= functionDuration)
        {
            duration  -= functionDuration;
            transitioning = true;
            transitionFunction = function;
            PickNextFunction();
        }

        UpdateFunctionOnGPU();
    }
    private void PickNextFunction()
    {
        function = transitionMode == TransitionMode.Cycle ? 
                                     FunctionLibrary.GetNextFunctionName(function) :
                                     FunctionLibrary.GetRandomFunctionName(function);
    }
    void UpdateFunctionOnGPU()
    {
        float step = 2f / resolution;
        float resolutionScale = Mathf.Lerp(lowResulutionScale, highResulutionScale, resolution / (float)maxResolution);

        computeShader.SetInt(resolutionId, resolution);
        computeShader.SetFloat(stepId, step);
        computeShader.SetFloat(timeId, Time.time);

        if(transitioning )
        {
            computeShader.SetFloat(
                transitionProgressId, 
                Mathf.SmoothStep(0f, 1f, duration / transitionDuration));
        }

        int kernelIndex = (int)function + 
                          (int)(transitioning ? transitionFunction : function) * 
                          FunctionLibrary.FunctionCount;

        computeShader.SetBuffer(kernelIndex, positionsId, positionsBuffer);
        int groups = Mathf.CeilToInt(resolution / 8f);
        computeShader.Dispatch(kernelIndex, groups, groups, 1);

        material.SetBuffer(positionsId,positionsBuffer);
        material.SetFloat(stepId, step);
        material.SetFloat(resolutionScaleId, resolutionScale);
        var bounds = new Bounds(Vector3.zero, Vector3.one * (2f + 2f / resolution));
        Graphics.DrawMeshInstancedProcedural(mesh, 0, material, bounds, resolution * resolution);
    }
}
