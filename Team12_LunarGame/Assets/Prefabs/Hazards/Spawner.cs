using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnlocations;
    public GameObject[] whatTospawnPrefab;
    public GameObject[] whatToSpawnClone;

    private void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        whatToSpawnClone[0] = Instantiate(whatTospawnPrefab[0], spawnlocations[0].transform.position, Quaternion.Euler(2, 0, 0)) as GameObject;
        whatToSpawnClone[0] = Instantiate(whatTospawnPrefab[0], spawnlocations[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[0] = Instantiate(whatTospawnPrefab[0], spawnlocations[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
    }
}
