using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    Transform point;
    [SerializeField,Range(10,100)]
    int resulution = 10;

    private void Awake()
    {
        var position = Vector3.zero;
        float step = 2f / resulution;
        var scale = Vector3.one * step;

        for (int i = 0; i < resulution; i++)
        {
            Transform p = Instantiate(point,transform);

            position.x = (i + 0.5f) * step - 1f;;
            position.y = position.x * position.x;
            p.localPosition = position;
            p.localScale = scale;
        }
    }
}
