using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "AudioObject")]
public class AudioObject : ScriptableObject
{
    public string Name;
    public List<AudioClip> Clips;
    public bool Looping;
    public bool RandomizeStartTime;
    public bool Persistant;
    [Range(0.0f, 1.0f)] public float Volume = 0.5f;
    public AudioMixerGroup Output;
    public float FadeOutTime;

    public AudioClip GetRandomClip()
    {
        return Clips.Count > 0 ? Clips[Random.Range(0, Clips.Count)] : null;
    }
}
