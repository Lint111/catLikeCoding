using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Mathf;

public static class FunctionLibrary
{
    public delegate Vector3 Function(float u, float v, float t);

    public enum FunctionName { Wave,MultiWave,Ripple,Sphere, Torus }

    private static readonly Function[] functions = { Wave, MultiWave, Ripple,Sphere, Torus };
    public static Function GetFunction(FunctionName name) => functions[(int)name];
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
        float r = 0.9f + 0.1f * Sin(PI * (6f * u + 4f * v  + t));
        float s = r * Cos(0.5f * PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r * Sin(PI * 0.5f * v);
        p.z = s * Cos(PI * u);
        return p;
    }
    public static Vector3 Torus(float u, float v, float t)
    {
        float r1 = 0.7f + 0.1f * Sin(PI * (6f * u + 0.5f * t));
        float r2 = 0.15f + 0.05f * Sin(PI * (8f * u + 4f * v + 2f * t));

        float s = r1 + r2 * Cos(PI * v);
        Vector3 p;
        p.x = s * Sin(PI * u);
        p.y = r2 * Sin(PI * v);
        p.z = s * Cos(PI * u);
        return p;
    }
}
