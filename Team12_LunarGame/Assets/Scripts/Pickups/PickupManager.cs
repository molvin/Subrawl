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
        public GameObject[] ObjectsToSpawn;
        public UnityEvent OnActivation;
        public UnityEvent<int> OnActivationWithPlayerId;
    }
    
    [SerializeField] private float _timeBetweenPickups = 10f;
    [SerializeField] private float _gameStartDelay = 3.0f;
    [SerializeField] private Pickup[] _pickups;
    [SerializeField] private PickupObject _pickUpObject;
    
    private float _timeOfLastPickup;
    
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
        int index = Random.Range(0, _pickups.Length);      
        Instantiate(_pickUpObject, GameManager.GetRandomSpawnPoint(), Quaternion.identity).OnPickup += id => OnPickUp(id, _pickups[index]);       
    }
    private void OnPickUp(int playerId, Pickup pickup)
    {
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
}
