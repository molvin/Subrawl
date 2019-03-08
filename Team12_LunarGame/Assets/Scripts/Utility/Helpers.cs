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