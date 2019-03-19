using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MinMaxFloat
{
    public float Min, Max;

    public float GetRandomValue()
    {
        return Random.Range(Min, Max);
    }
    public float Lerp(float lerpValue)
    {
        return Mathf.Lerp(Min, Max, Mathf.Clamp01(lerpValue));
    }
}
[System.Serializable]
public struct Tuple<T1, T2>
{
    public T1 First;
    public T2 Second;
}

public static class Extensions
{
    public static Color WithAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}