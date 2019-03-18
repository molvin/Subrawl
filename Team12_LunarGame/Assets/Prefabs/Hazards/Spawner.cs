using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnlocations;
    public GameObject[] whatTospawnPrefab;
    public PlayAnimation[] Animators;
    public float DelayBeforeSpawn;

    private void Start()
    {
        //Animator.PlayAnim();
        //StartCoroutine(DelayOnSpawn());
    }

    public void SpawnRoutine()
    {
        foreach (PlayAnimation animator in Animators)
        {
            animator.PlayAnim();
        }
        StartCoroutine(DelayOnSpawn());
    }
    void SpawnBubbles()
    {
        Instantiate(whatTospawnPrefab[0], spawnlocations[0].transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(whatTospawnPrefab[0], spawnlocations[1].transform.position, Quaternion.Euler(0, 0, 0));
        Instantiate(whatTospawnPrefab[0], spawnlocations[2].transform.position, Quaternion.Euler(0, 0, 0));
        //SpawnRoutine();
    }

    IEnumerator DelayOnSpawn()
    {
        yield return new WaitForSeconds(DelayBeforeSpawn);
        foreach (PlayAnimation animator in Animators)
        {
            animator.StopAnim();
        }
        SpawnBubbles();
    }

}
