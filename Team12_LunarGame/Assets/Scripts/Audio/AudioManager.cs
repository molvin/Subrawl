using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    [SerializeField] private AudioSource _audioSourcePrefab;
    
    private List<AudioObject> _audioObjects = new List<AudioObject>();
    private Dictionary<string, AudioObject> _audioObjectDictionary;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _audioObjects = Resources.LoadAll<AudioObject>("").ToList();
        BuildDictionary();
    }
    public static void PlaySound(string audioObjectName)
    {
        if (_instance == null)
        {
            _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
            _instance._audioObjects = Resources.LoadAll<AudioObject>("").ToList();
        }
        if(_instance._audioObjectDictionary == null)
            _instance.BuildDictionary();

        if (!_instance._audioObjectDictionary.ContainsKey(audioObjectName))
        {
            Debug.LogWarning("No audio object with name: " + audioObjectName);
            return;
        }
        
        //TODO: play audio object sound
    }
    private void BuildDictionary()
    {
        _audioObjectDictionary = new Dictionary<string, AudioObject>();
        
        foreach (AudioObject ao in _audioObjects)
            _audioObjectDictionary.Add(ao.Name, ao);        
    }
}
