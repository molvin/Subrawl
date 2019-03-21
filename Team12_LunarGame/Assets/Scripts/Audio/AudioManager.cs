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
    private readonly List<AudioSource> _playingSources = new List<AudioSource>();
    
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
    private void Update()
    {
        List<AudioSource> destroyList = new List<AudioSource>();
        foreach (AudioSource source in _playingSources)
        {
            if(!source.isPlaying)
                destroyList.Add(source);
        }

        foreach (AudioSource source in destroyList)
        {
            _playingSources.Remove(source);
            Destroy(source.gameObject);
        }
        _playingSources.Clear();
    }
    public static AudioRemote PlaySound(string audioObjectName)
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
            return null;
        }
        
        AudioObject ao = _instance._audioObjectDictionary[audioObjectName];
        AudioSource source = Instantiate(_instance._audioSourcePrefab);
        source.clip = ao.GetRandomClip();
        source.loop = ao.Looping;
        source.volume = ao.Volume;
        if (ao.RandomizeStartTime)
            source.time = Random.Range(0.0f, source.clip.length);
        source.Play();
        if(ao.Persistant)
            DontDestroyOnLoad(source.gameObject);
        _instance._playingSources.Add(source);
        
        return new AudioRemote(source);
    }
    private void BuildDictionary()
    {
        _audioObjectDictionary = new Dictionary<string, AudioObject>();
        
        foreach (AudioObject ao in _audioObjects)
            _audioObjectDictionary.Add(ao.Name, ao);        
    }
}
public class AudioRemote
{
    private readonly AudioSource _source;

    public AudioRemote(AudioSource source)
    {
        _source = source;
    }
    public void Stop()
    {
        _source.Stop();
    }
}
