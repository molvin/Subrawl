using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PickupManager : MonoBehaviour
{
    [System.Serializable]
    public class Pickup
    {
        public string Name;
        public Sprite Icon;
        public GameObject[] ObjectsToSpawn;
        public UnityEvent OnActivation;
        public UnityEvent<int> OnActivationWithPlayerId;
    }
    
    [SerializeField] private float _timeBetweenPickups = 10f;
    [SerializeField] private float _gameStartDelay = 5.0f;
    [SerializeField] private Pickup[] _pickups;
    [SerializeField] private PickupObject _pickUpObject;
    
    private float _timeOfLastPickup;
    private int _currentPickupOwner;
    public Pickup[] Pickups => _pickups;
    private int _nextPickup;
    
    private void Start()
    {
        _timeOfLastPickup = Time.time - (_timeBetweenPickups - _gameStartDelay);
    }
    private void Update()
    {
        if (Time.time - _timeOfLastPickup < _timeBetweenPickups) return;
        SpawnPickUp();
        _timeOfLastPickup = Time.time;
    }
    private void SpawnPickUp()
    {
        int index = _nextPickup;
        _nextPickup = (_nextPickup + 1) % _pickups.Length;
        PickupObject obj = Instantiate(_pickUpObject, GameManager.GetRandomSpawnPoint(), Quaternion.identity);
        Sprite icon = _pickups[index].Icon;

        if(icon != null)
            obj.GetComponentInChildren<SpriteRenderer>().sprite = icon;
        
        obj.OnPickup += id => OnPickUp(id, _pickups[index]);

    }
    public void OnPickUp(int playerId, Pickup pickup)
    {
        AudioManager.PlaySound("PowerUp");
        _currentPickupOwner = playerId;
        Debug.Log("Activated: " + pickup.Name);
        foreach (GameObject go in pickup.ObjectsToSpawn)
            Instantiate(go);
        pickup.OnActivation?.Invoke();
        pickup.OnActivationWithPlayerId?.Invoke(playerId);
    }

    public float GetTimeRemaining()
    {
        return _timeBetweenPickups - (Time.time - _timeOfLastPickup);
    }

    public Pickup GetPickUp(string pickupName)
    {
        return _pickups.FirstOrDefault(pickup => pickup.Name == pickupName);
    }

    public int GetCurrentPickupOwner()
    {
        return _currentPickupOwner;
    }
}
