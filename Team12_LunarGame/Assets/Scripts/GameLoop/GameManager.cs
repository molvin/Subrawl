using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private int _playerMaxHealth;
    
    public static readonly Dictionary<int, int> PlayerHealth = new Dictionary<int, int>();
    
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            return;
        
        PlayerHealth.Add(0, _playerMaxHealth);
        PlayerHealth.Add(1, _playerMaxHealth);
    }
    public static void HandlePlayerDeath(int deadPlayerId)
    {
        if (!PlayerHealth.ContainsKey(deadPlayerId))
            return;

        PlayerHealth[deadPlayerId]--;
        
    }
}
