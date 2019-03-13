using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PongManager : MonoBehaviour
{
    [SerializeField] private GameObject _paddlePrefab;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private float _ballSpeed = 15.0f;
    [SerializeField] private float _paddleXDistanceFromCenter = 10.0f;
    [SerializeField] private LayerMask _ballCollisionLayers;
    [SerializeField] private float _duration = 8f;
    
    private readonly List<GameObject> _paddles = new List<GameObject>();
    private GameObject _ball;
    private Vector2 _ballVelocity;
    private float _ballRadius;
    private bool _active;
    private float _startTime;
    
    public void Play()
    {
        ResetPong();

        _paddles.Add(Instantiate(_paddlePrefab, new Vector3(_paddleXDistanceFromCenter, 0.0f, 0.0f), Quaternion.identity));
        _paddles.Add(Instantiate(_paddlePrefab, new Vector3(-_paddleXDistanceFromCenter, 0.0f, 0.0f), Quaternion.identity));
        _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        _ballRadius = _ball.GetComponent<CircleCollider2D>().radius;
        _ballVelocity = Quaternion.Euler(0.0f, 0.0f, Random.Range(-45, 45)) * Vector2.right * _ballSpeed;
        _active = true;
        _startTime = Time.time;
    }

    private void Update()
    {
        if (!_active) return;
      
        RaycastHit2D hit = Physics2D.CircleCast(_ball.transform.position, _ballRadius, _ballVelocity.normalized, _ballVelocity.magnitude * Time.deltaTime, _ballCollisionLayers);
        if (hit.collider != null)
        {
            _ballVelocity = Vector2.Reflect(_ballVelocity, hit.normal);
            PlayerValues player = hit.collider.GetComponent<PlayerValues>();
            if(player != null)
                player.Die();
        }

        _ball.transform.position += (Vector3) _ballVelocity * Time.deltaTime;
        
        foreach (GameObject paddle in _paddles)
        {
            Vector3 position = paddle.transform.position;
            position.y = _ball.transform.position.y;
            paddle.transform.position = position;
        }

        if (Time.time - _startTime > _duration)
            ResetPong();
    }

    private void ResetPong()
    {
        Debug.Log("Reset pong");
        foreach (GameObject go in _paddles)
        {
            Destroy(go);
        }
        if(_ball != null) 
            Destroy(_ball);

        _ball = null;
        _paddles.Clear();
        _active = false;
    }

}
