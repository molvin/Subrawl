using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PacManAIFollow : MonoBehaviour{

    public GameObject player;
    public GameObject player2;
    public float speed;
    private Transform target;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name == "Player 1")
        {
            player.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(this.gameObject, 0.5f);
        }

        else if (other.gameObject.name == "Player 2")
        {
            player2.GetComponent<PlayerHealth>().TakeDamage();
            Destroy(this.gameObject, 0.5f);
        }

        // If AI is dodged constantly it will "time out" and die after 15 seconds.
        Destroy(this.gameObject, 15f);

    }



    void Start()
    {
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player2 = GameObject.FindGameObjectWithTag("Player2");
        }
        //if (Player1PickedUpTheThing == true)
        //{
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        }

    //    else
    //    {
    //     target = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();
    //    }
    //}

    void Update(){

    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        
    }

}
