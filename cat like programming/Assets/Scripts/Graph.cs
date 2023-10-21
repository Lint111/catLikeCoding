using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform point;
    [SerializeField,Range(10,200)]
    int resulution = 10;

    [SerializeField]
    FunctionLibrary.FunctionName function;

    Transform[] points;

    private void Awake()
    {
        points = new Transform[resulution * resulution];
        float step = 2f / resulution;
        var scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {
            Transform p = points[i] = Instantiate(point, transform);
            p.localScale = scale;
        }
    }
    private void Update()
    {
        FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);
        float time = Time.time;
        float step = 2f / resulution;
        float v = 0.25f * step - 1f;
        for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
        {
            if (x == resulution)
            {
                x = 0;
                z++;
                v = (z + 0.5f) * step - 1f;
            }

            float u = (x + 0.5f) * step - 1f;
            points[i].localPosition = f(u, v, time);
        }
    }
}
