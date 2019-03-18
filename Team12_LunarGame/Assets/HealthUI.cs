using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthImagePrefab;
    [SerializeField] private Transform _player1HealthParent;
    [SerializeField] private Transform _player2HealthParent;
    
    private readonly Dictionary<int, List<Image>> _healthImages = new Dictionary<int, List<Image>>();

    private void Start()
    {
        SpawnHealth(0);
        SpawnHealth(1);
        GameManager.OnLifeUpdate += UpdateLives;
    }
    private void SpawnHealth(int id)
    {
        _healthImages[id] = new List<Image>();
        Transform healthParent = id == 0 ? _player1HealthParent : _player2HealthParent;
        for (int i = 0; i < GameManager.MaxLives; i++)
        {
            Image instance = Instantiate(_healthImagePrefab, healthParent);
            _healthImages[id].Add(instance);
        }
    }
    private void UpdateLives(int id, int lives)
    {
        List<Image> health = _healthImages[id];
        for(int i = 0; i < health.Count; i++)
        {
            health[i].enabled = i < lives;
        }
    }
    
}
