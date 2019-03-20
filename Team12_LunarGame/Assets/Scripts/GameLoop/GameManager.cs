using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Sprite _player1Sprite;
    [SerializeField] private Sprite _player2Sprite;
    [SerializeField] private int _playerMaxLives = 5;
    [SerializeField] private Vector2[] _playerSpawnPoints;
    [SerializeField] private float _spawnPositionEdgeBuffer = 50.0f;
    [SerializeField] private float _respawnTime = 2.0f;
    
    public static readonly Dictionary<int, int> PlayerLives = new Dictionary<int, int>();
    public static Action OnLivesChanged;
    public static Action<int, int> OnLifeUpdate;
    public static Action<int> OnVictory;
    public static int MaxLives => _instance._playerMaxLives;
    public float SpawnPositionEdgeBuffer => _spawnPositionEdgeBuffer;
    public Vector2[] PlayerSpawnPoints => _playerSpawnPoints;

    private bool _gameOver;
    
    private void Awake()
    {
        _instance = this;
        OnLivesChanged = null;
        OnLifeUpdate = null;
        OnVictory = null;
        ResetLives();

        if (FindObjectOfType<PlayerValues>() == null)
        {
            StartCoroutine(SpawnRoutine(0, 5));
            StartCoroutine(SpawnRoutine(1, 5));
        }
        
    }
    public static void HandlePlayerDeath(int deadPlayerId)
    {
        if (!PlayerLives.ContainsKey(deadPlayerId))
            return;

        PlayerLives[deadPlayerId]--;
        if (PlayerLives[deadPlayerId] > 0)
            _instance.StartCoroutine(_instance.SpawnRoutine(deadPlayerId, _instance._respawnTime));

        if (!_instance._gameOver && PlayerLives[deadPlayerId] == 0)
        {
            _instance._gameOver = true;
            OnVictory?.Invoke(deadPlayerId == 0 ? 1 : 0);
        }
        
        OnLivesChanged?.Invoke();
        OnLifeUpdate?.Invoke(deadPlayerId, PlayerLives[deadPlayerId]);
        
    }

    private IEnumerator SpawnRoutine(int id, float time)
    {
        yield return new WaitForSeconds(time);
        SpawnPlayer(id);
    }
    private void SpawnPlayer(int id)
    {

        GameObject instance = Instantiate(_playerPrefab, PlayerSpawnPoints.Length > id ? PlayerSpawnPoints[id] : GetRandomSpawnPoint(), Quaternion.identity);
        PlayerValues playerValues = instance.GetComponent<PlayerValues>();
        playerValues.Id = id;
        playerValues.Invincible = true;
        instance.GetComponent<PlayerMovement>().RewiredId = id;
        instance.GetComponentInChildren<SpriteRenderer>().sprite = id == 0 ? _player1Sprite : _player2Sprite;
        //Temp
        //  instance.GetComponentInChildren<SpriteRenderer>().color = id == 0 ? Color.blue : Color.red;
    }
    public static Vector2 GetRandomSpawnPoint()
    {
        float height = Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        Vector2 randomSize = new Vector2(width - _instance._spawnPositionEdgeBuffer * Camera.main.aspect, height -  _instance._spawnPositionEdgeBuffer);
        return new Vector2(UnityEngine.Random.Range(-randomSize.x, randomSize.x), UnityEngine.Random.Range(-randomSize.y, randomSize.y));
    }

    public static void ResetLives()
    {
        PlayerLives.Clear();
        PlayerLives.Add(0, _instance._playerMaxLives);
        PlayerLives.Add(1, _instance._playerMaxLives);
        OnLivesChanged?.Invoke();
        OnLifeUpdate?.Invoke(0, _instance._playerMaxLives);
        OnLifeUpdate?.Invoke(1, _instance._playerMaxLives);
    }
}
