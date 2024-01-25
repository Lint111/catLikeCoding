using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName { Wave,MultiWave,Ripple,Sphere, Torus }

    private static readonly Function[] functions = { Wave, MultiWave, Ripple,Sphere, Torus };
    public static int FunctionCount => functions.Length;
    public static Function GetFunction(FunctionName name) => functions[(int)name];
    public static FunctionName GetNextFunctionName (FunctionName functionName) => (int)functionName < functions.Length - 1 ? functionName + 1 : 0;
    public static FunctionName GetRandomFunctionName(FunctionName functionName)
    {
        var choice = (FunctionName)Random.Range(1, functions.Length);
        return choice == functionName ? 0 : choice;
    }
    public static Vector3 Morph(float u, float v, float t, Function from, Function to,float progress) => Vector3.LerpUnclamped(from(u,v,t),to(u,v,t), SmoothStep(0f,1f,progress));
    
    public static Vector3 Wave(float u, float v, float t) => new Vector3(u, Sin(PI * (u + v + t)), v);
    public static Vector3 MultiWave(float u, float v, float t)
    {
        float f = Sin(PI * (u + 0.5f * t));
        f += 0.5f * Sin(2f * PI * (v + t));
        f += Sin(PI * (u + v + 0.25f * t));
        return new Vector3(u, f * (1f / 2.5f), v);
    }
    public static Vector3 Ripple(float u, float v, float t)
    {
        float d = Sqrt(u * u + v * v);
        float y = Sin(PI * (4f * d - t));
        return new Vector3(u, y / (1f + 10f * d), v);
    }
    public static Vector3 Sphere(float u, float v, float t)
    {
        float r = 0.9f + 0.1f * Sin(PI * (12f * u + 8f * v  + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }
    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (8f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (16f * u + 8f * v + 3f * t));

        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
