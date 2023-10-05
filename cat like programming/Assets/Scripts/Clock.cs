using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform hourPivot,minutePivot,sescondPivot;

    const float HOURS_TO_DEGREES = -30f, MINUTES_TO_DEGREES = -6f, SECONDS_TO_DEGREES = -6f;

    private void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hourPivot.localRotation = Quaternion.Euler(0f, 0f, HOURS_TO_DEGREES * (float)time.TotalHours);
        minutePivot.localRotation = Quaternion.Euler(0f, 0f, MINUTES_TO_DEGREES * (float)time.TotalMinutes);
        sescondPivot.localRotation = Quaternion.Euler(0f, 0f, SECONDS_TO_DEGREES * (float)time.TotalSeconds);
    }
}
