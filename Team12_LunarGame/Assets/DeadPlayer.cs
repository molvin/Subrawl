using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 _positionOffset;
    [SerializeField] private float _scale;
    
    private void Start()
    {
        transform.position += (Vector3) _positionOffset;
        transform.localScale = Vector3.one * _scale;
    }
}
