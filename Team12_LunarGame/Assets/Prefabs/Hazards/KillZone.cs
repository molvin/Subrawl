using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public GameObject Bubbles;


    private void Start()
    {
        Bubbles = GameObject.FindGameObjectWithTag("Coral");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("yo");

        if (other.CompareTag("Coral"))
        {
            Destroy(Bubbles);
        }
    }
}
