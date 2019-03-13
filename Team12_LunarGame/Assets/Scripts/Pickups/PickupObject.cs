using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Action<int> OnPickup;
    private bool _triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_triggered)
            return;
        PlayerValues player = other.GetComponent<PlayerValues>();
        if (player == null)
            return;

        _triggered = true;
        OnPickup.Invoke(player.Id);
        Destroy(gameObject);
    }
}
