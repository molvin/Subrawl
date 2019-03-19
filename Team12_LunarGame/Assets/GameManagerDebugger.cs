using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameManagerDebugger : MonoBehaviour
{
    [SerializeField] private bool _drawSpawnBox;
    [SerializeField] private bool _drawPlayerSpawns;
    [SerializeField] private Color _debugColor;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }
    
    private void OnDrawGizmos()
    {
        if (_drawPlayerSpawns)
        {
            Gizmos.color = Color.blue.WithAlpha(0.5f);
            if(_gameManager.PlayerSpawnPoints.Length > 0)
                Gizmos.DrawCube(_gameManager.PlayerSpawnPoints[0], Vector2.one);
            Gizmos.color = Color.red.WithAlpha(0.5f);
            if(_gameManager.PlayerSpawnPoints.Length > 1)
                Gizmos.DrawCube(_gameManager.PlayerSpawnPoints[1], Vector2.one);
        }
        if (_drawSpawnBox)
        {
            Gizmos.color = _debugColor;

            float height = Camera.main.orthographicSize * 2f;
            float width = height * Camera.main.aspect;
            float xBuffer = _gameManager.SpawnPositionEdgeBuffer * Camera.main.aspect;
            float yBuffer = _gameManager.SpawnPositionEdgeBuffer;
            Vector3 size = new Vector3(width - xBuffer * 2, height - yBuffer * 2, 1f);
            Gizmos.DrawCube(Vector3.zero, size);
        }    
    }
}
