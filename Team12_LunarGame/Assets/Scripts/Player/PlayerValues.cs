using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    public int Id;
    public bool Invincible;
    public Animator AnimationController;

    [SerializeField] private float _invincibilityDuration;
    private float _currentInvincibilityTime;
    
    private static readonly Dictionary<int, PlayerValues> _players = new Dictionary<int, PlayerValues>();

    private void Start()
    {

        if (Invincible)
        {
            AnimationController.SetBool("Damaged", true);
        }

        if (_players.ContainsKey(Id))
            _players[Id] = this;
        else
            _players.Add(Id, this);

        if (Invincible)
        {
            AnimationController.SetBool("Damaged", true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Id == 0 || Input.GetKeyDown(KeyCode.P) && Id == 1)
            Die();

        if (!Invincible)
            return;
        
        _currentInvincibilityTime += Time.deltaTime;
        if (_currentInvincibilityTime > _invincibilityDuration)
        {
            //Stop animation
            Invincible = false;
        }
    }
    
    public void Die()
    {
        if (Invincible) 
            return;
        GameManager.HandlePlayerDeath(Id);
        _players.Remove(Id);
        Destroy(gameObject);
    }
    public static PlayerValues GetPlayer(int id)
    {
        return _players.ContainsKey(id) ? _players[id] : null;
    }
}
