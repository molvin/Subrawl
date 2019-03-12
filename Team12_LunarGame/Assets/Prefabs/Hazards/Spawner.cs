using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnlocations;
    public GameObject[] whatTospawnPrefab;
    public GameObject[] whatToSpawnClone;
    public GameObject Coral;

    private void Start()
    {
        SpawnBubbles();
        Coral = GameObject.FindGameObjectWithTag("Coral");
    }

    void SpawnBubbles()
    {
        whatToSpawnClone[0] = Instantiate(whatTospawnPrefab[0], spawnlocations[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[0] = Instantiate(whatTospawnPrefab[0], spawnlocations[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[0] = Instantiate(whatTospawnPrefab[0], spawnlocations[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        StartCoroutine(DelayOnSpawn());
    }

    IEnumerator DelayOnSpawn()
    {
        yield return new WaitForSeconds(20);
        SpawnBubbles();
    }

}
