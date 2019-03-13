using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PacManAIFollow : MonoBehaviour{

    public float speed;
    private Transform target;
    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    //Check collision name
    //    Debug.Log("collision name = " + col.gameObject.name);
    //    if (col.gameObject.name == "Player1")
    //    {
    //    gib dmg here to player1??
    //    Destroy(this.GameObject,0.5f);
    //    }
    //    if (col.gameObject.name == "Player2")
    //    {
    //    gib dmg here to player2??
    //        Destroy(this.GameObject,0.5f);
    //    }
    //}



    void Start()
    {
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
