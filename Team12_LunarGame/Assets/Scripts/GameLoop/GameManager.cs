using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private int _playerMaxLives = 5;
    
    public static readonly Dictionary<int, int> PlayerLives = new Dictionary<int, int>();
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            return;
        
        PlayerLives.Add(0, _playerMaxLives);
        PlayerLives.Add(1, _playerMaxLives);
    }
    public static void HandlePlayerDeath(int deadPlayerId)
    {
        if (!PlayerLives.ContainsKey(deadPlayerId))
            return;

        PlayerLives[deadPlayerId]--;

        if (PlayerLives[deadPlayerId] <= 0) return;
        
        _instance.SpawnPlayer(deadPlayerId);    
    }
    private void SpawnPlayer(int id)
    {
        GameObject instance = Instantiate(_playerPrefab, Vector2.zero, Quaternion.identity);
        instance.GetComponent<PlayerValues>().Id = id;
        instance.GetComponent<PlayerMovement>().RewiredId = id;
        instance.GetComponentInChildren<SpriteRenderer>().color = id == 0 ? Color.blue : Color.red;
    }
}
