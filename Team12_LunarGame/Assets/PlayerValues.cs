using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    public int Id;
    
    private static readonly Dictionary<int, PlayerValues> _players = new Dictionary<int, PlayerValues>();

    private void Start()
    {
        if (_players.ContainsKey(Id))
            _players[Id] = this;
        else
            _players.Add(Id, this);
        
        gameObject.layer = Id == 0 ? LayerMask.NameToLayer("Player 1") : LayerMask.NameToLayer("Player 2");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Id == 0 || Input.GetKeyDown(KeyCode.P) && Id == 1)
            Die();
    }
    
    public void Die()
    {
        GameManager.HandlePlayerDeath(Id);
        _players.Remove(Id);
        Destroy(gameObject);
    }
    public static PlayerValues GetPlayer(int id)
    {
        return _players.ContainsKey(id) ? _players[id] : null;
    }
}
