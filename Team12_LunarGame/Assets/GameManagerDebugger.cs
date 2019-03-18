using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GameManagerDebugger : MonoBehaviour
{
    [SerializeField] private bool _drawSpawnBox;
    [SerializeField] private Color _debugColor;

    private GameManager _gameManager;
    
    private void Start()
    {
        _gameManager = GetComponent<GameManager>();
    }
    
    private void OnDrawGizmos()
    {
        if (!_drawSpawnBox) return;
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Camera.main.aspect;
        float xBuffer = _gameManager.SpawnPositionEdgeBuffer * Camera.main.aspect;
        float yBuffer = _gameManager.SpawnPositionEdgeBuffer;
        Vector3 size = new Vector3(width - xBuffer * 2, height - yBuffer * 2, 1f);
        Gizmos.color = _debugColor;
        Gizmos.DrawCube(Vector3.zero, size);
    }
}
