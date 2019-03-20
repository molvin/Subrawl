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
    [SerializeField] private float _paddleStartY;
    [SerializeField] private LayerMask _ballCollisionLayers;
    [SerializeField] private float _duration = 8f;
    [SerializeField] private float _maxRandomAngle;
    
    private readonly List<GameObject> _paddles = new List<GameObject>();
    private GameObject _ball;
    private Vector2 _ballVelocity;
    private float _ballRadius;
    private bool _active;
    private float _startTime;
    
    public void Play()
    {
        ResetPong();

        _paddles.Add(Instantiate(_paddlePrefab, new Vector3(_paddleXDistanceFromCenter, _paddleStartY, 0.0f), Quaternion.identity));
        _paddles.Add(Instantiate(_paddlePrefab, new Vector3(-_paddleXDistanceFromCenter, _paddleStartY, 0.0f), Quaternion.identity));
        _ball = Instantiate(_ballPrefab, Vector3.zero, Quaternion.identity);
        _ballRadius = _ball.GetComponent<CircleCollider2D>().radius;
        int ballStartOwner = Random.value > 0.5f ? 0 : 1;
        Vector2 directionAwayFromOwner = Vector2.right * (ballStartOwner == 0 ? -1 : 1);
        _ball.transform.position = (Vector2)_paddles[ballStartOwner].transform.position + directionAwayFromOwner * _ballRadius * 5f;
        _ballVelocity = Quaternion.Euler(0.0f, 0.0f, Random.Range(-45, 45)) * directionAwayFromOwner  * _ballSpeed;
        _active = true;
        _startTime = Time.time;
        _paddles[1].transform.GetChild(0).transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Update()
    {
        if (!_active) return;
      
        RaycastHit2D hit = Physics2D.CircleCast(_ball.transform.position, _ballRadius, _ballVelocity.normalized, _ballVelocity.magnitude * Time.deltaTime, _ballCollisionLayers);
        if (hit.collider != null)
        {
            if(_paddles.Contains(hit.collider.gameObject))
                _ballVelocity = Quaternion.Euler(0.0f, 0.0f, Random.Range(_maxRandomAngle, _maxRandomAngle)) * _ballVelocity;
           
            PlayerValues player = hit.collider.GetComponent<PlayerValues>();
            if(player != null)
            {
                player.Die();
                ResetPong();
            }
                

            else
                _ballVelocity = Vector2.Reflect(_ballVelocity, hit.normal);

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
