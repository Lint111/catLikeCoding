using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform hourPivot,minutePivot,sescondPivot;

    private void Awake()
    {
        hourPivot.localRotation = Quaternion.Euler(0,0,-30);
    }
}
