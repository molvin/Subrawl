using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private int _playerMaxLives = 5;
    [SerializeField] private float _spawnPositionEdgeBuffer = 50.0f;

    [SerializeField] private LayerMask Player1CollisionLayers;
    [SerializeField] private LayerMask Player2CollisionLayers;
    
    public static readonly Dictionary<int, int> PlayerLives = new Dictionary<int, int>();
    public static Action OnLivesChanged;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            return;

        ResetLives();
    }
    public static void HandlePlayerDeath(int deadPlayerId)
    {
        if (!PlayerLives.ContainsKey(deadPlayerId))
            return;

        PlayerLives[deadPlayerId]--;
        OnLivesChanged?.Invoke();

        if (PlayerLives[deadPlayerId] <= 0) return;
        
        _instance.SpawnPlayer(deadPlayerId);    
    }
    private void SpawnPlayer(int id)
    {
        GameObject instance = Instantiate(_playerPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        instance.GetComponent<PlayerValues>().Id = id;
        instance.GetComponent<PlayerMovement>().RewiredId = id;
        instance.GetComponentInChildren<SpriteRenderer>().color = id == 0 ? Color.blue : Color.red;
        instance.GetComponent<PlayerMovement>().CollisionLayers = id == 0 ? Player1CollisionLayers : Player2CollisionLayers;
    }
    public static Vector2 GetRandomSpawnPoint()
    {
        float width = Camera.main.orthographicSize * 2f;
        float height = width / Camera.main.aspect;
        Vector2 randomSize = new Vector2(width - _instance._spawnPositionEdgeBuffer, height -  _instance._spawnPositionEdgeBuffer / Camera.main.aspect);
        return new Vector2(UnityEngine.Random.Range(-randomSize.x, randomSize.x), UnityEngine.Random.Range(-randomSize.y, randomSize.y));
    }

    public static void ResetLives()
    {
        PlayerLives.Clear();
        PlayerLives.Add(0, _instance._playerMaxLives);
        PlayerLives.Add(1, _instance._playerMaxLives);
        OnLivesChanged?.Invoke();
    }
}
