using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PacManAIFollow : MonoBehaviour{

    public GameObject player;
    public GameObject player2;
    public float speed;
    public Transform target;
    public Animator animator;
    public float rotationSpeed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerValues player = other.gameObject.GetComponent<PlayerValues>();
        if (player != null)
        {
            player.Die();
            Destroy(gameObject, 0.5f);
        }
//        if(player == null || player.IsPlayerDead)
//        {
//            Destroy(this.gameObject);
//        }

        //if (other.gameObject.name == "Player 1")
        //{
        //    player.GetComponent<PlayerValues>().Die();
        //    Destroy(this.gameObject, 0.5f);
        //}

        //else if (other.gameObject.name == "Player 2")
        //{
        //    player2.GetComponent<PlayerValues>().Die();

        //    Destroy(this.gameObject, 0.5f);
        //}

    }



    void Start()
    {
        // If AI is dodged constantly it will "time out" and die after 15 seconds.
        Destroy(this.gameObject, 15f);
        StartCoroutine(TimeOut(10));

        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");

        PickupManager manager = FindObjectOfType<PickupManager>();
        int currentPickupOwner = manager.GetCurrentPickupOwner();
        if (currentPickupOwner == 0)
            target = PlayerValues.GetPlayer(1)?.transform;
        else
            target = PlayerValues.GetPlayer(0)?.transform;
        animator.SetBool("Targeted", true);
        target.GetComponent<PlayerValues>()?.SetTargeted(true);
        //if (Player1PickedUpTheThing == true)
        //{
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
    }

    //    else
    //    {
    //     target = GameObject.FindGameObjectWithTag("Player2").GetComponent<Transform>();
    //    }
    //}

    IEnumerator TimeOut(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        animator.SetBool("Dying", true);
        print("HELP");
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
            

        Vector2 toTarget = target.position - transform.position;
        Vector3 scale = Vector3.one;
        scale.y = toTarget.x < 0.0f ? -1 : 1;
        transform.GetChild(0).localScale = scale;
        
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * rotationSpeed);

    }

    private void OnDestroy()
    {
        if (target != null)
            target.GetComponent<PlayerValues>().SetTargeted(false);
    }
}
