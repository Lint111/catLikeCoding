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

    Transform[] points;

    private void Awake()
    {
        points = new Transform[resulution];
        var position = Vector3.zero;
        float step = 2f / resulution;
        var scale = Vector3.one * step;

        for (int i = 0; i < points.Length; i++)
        {
            Transform p = points[i] = Instantiate(point, transform);
            position.x = (i + 0.5f) * step - 1f;
            position.y = position.x * position.x * position.x;
            p.localPosition = position;
            p.localScale = scale;

        }
    }
    private void Update()
    {
        float time = Time.time;
        for (int i = 0; i < points.Length; i++)
        {
            Transform p = points[i];
            Vector3 position = p.localPosition;
            position.y = Mathf.Sin(Mathf.PI * (position.x + time));
            p.localPosition = position;
        }
    }
}
