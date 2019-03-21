using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class PlayerValues : MonoBehaviour
{
    public int Id;
    public bool Invincible;
    public bool IsPlayerDead = false;
    public Animator AnimationController;

    [SerializeField] private float _invincibilityDuration;
    private float _currentInvincibilityTime;
    
    private static readonly Dictionary<int, PlayerValues> _players = new Dictionary<int, PlayerValues>();

    public Action OnDeath;
    
    private bool _playingSound;
    private AudioRemote _audioRemote;

    
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
        IsPlayerDead = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Id == 0 || Input.GetKeyDown(KeyCode.P) && Id == 1)
            Die();


        float vertical = ReInput.players.GetPlayer(Id).GetAxisRaw("Vertical");
        if (Mathf.Abs(vertical) > 0.5f)
        {
            if (!_playingSound)
            {
                _playingSound = true;
                _audioRemote = AudioManager.PlaySound("Accelerate");
            }
        }
        else
        {
            if (_playingSound)
            {
                _audioRemote.FadeOut();
                _playingSound = false;
            }
        }
        
        if (!Invincible)
            return;
        
        _currentInvincibilityTime += Time.deltaTime;
        if (_currentInvincibilityTime > _invincibilityDuration)
        {
            //Stop animation
            AnimationController.SetBool("Damaged", false);
            Invincible = false;
        }
    }
    
    public void Die()
    {
        if (Invincible) 
            return;
        
        if(_playingSound)
            _audioRemote.FadeOut();
        AudioManager.PlaySound("Explosion");
        GameManager.HandlePlayerDeath(Id, transform.position);
        IsPlayerDead = true;
        _players.Remove(Id);
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
    public static PlayerValues GetPlayer(int id)
    {
        return _players.ContainsKey(id) ? _players[id] : null;
    }
}
