using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float Health = 0;

    void Update()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            
        }
    }

    public void TakeDamage()
    {
        Health = Health - 1;
    }
}
