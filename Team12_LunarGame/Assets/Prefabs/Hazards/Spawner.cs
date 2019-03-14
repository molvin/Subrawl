using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnlocations;
    public GameObject[] whatTospawnPrefab;
    public PlayAnimation Animator;

    private void Start()
    {
        Animator.Hello();
        Animator.PlayAnim();
        SpawnBubbles();
    }

    void SpawnBubbles()
    {
        Instantiate(whatTospawnPrefab[0], spawnlocations[0].transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(whatTospawnPrefab[0], spawnlocations[1].transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(whatTospawnPrefab[0], spawnlocations[2].transform.position, Quaternion.Euler(0, 0, 0));
        StartCoroutine(DelayOnSpawn());
    }

    IEnumerator DelayOnSpawn()
    {
        yield return new WaitForSeconds(5);
        SpawnBubbles();
    }

}
