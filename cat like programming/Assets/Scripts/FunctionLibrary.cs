using UnityEngine;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public static float Wave(float x, float t) => Sin(PI * (x + t));
    public static float MultiWave(float x, float t) => (Sin(PI * (x + 0.5f * t)) + 
                                                 0.5f * Sin(2f * PI * (x + t))) * (2f / 3f);
    public static float Ripple(float x, float t)
    {
        float d = Abs(x);
        float y = Sin(PI * (4f * d - t));
        return y / (1f + 10f * d);

    }
}
