using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioObject")]
public class AudioObject : ScriptableObject
{
    public string Name;
    public List<AudioClip> Clips;

    public AudioClip GetRandomClip()
    {
        return Clips[Random.Range(0, Clips.Count)];
    }
}
