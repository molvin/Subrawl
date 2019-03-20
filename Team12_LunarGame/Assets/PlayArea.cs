using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayArea : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;

    private float MaxX => _collider.bounds.size.x;
    private float MinX => -_collider.bounds.size.x;
    private float MaxY => _collider.bounds.size.y;
    private float MinY => -_collider.bounds.size.y;
    
    private void Update()
    {
        PlayerValues player1 = PlayerValues.GetPlayer(0);
        if (player1 != null)
        {
            float x = player1.transform.position.x;
            float y = player1.transform.position.y;
            if (x > MaxX || x < MinX || y > MaxY || y < MinY)
                player1.Die();
        }
        PlayerValues player2 = PlayerValues.GetPlayer(1);
        if (player2 != null)
        {
            float x = player2.transform.position.x;
            float y = player2.transform.position.y;
            if (x > MaxX || x < MinX || y > MaxY || y < MinY)
                player2.Die();
        }
    }
}
