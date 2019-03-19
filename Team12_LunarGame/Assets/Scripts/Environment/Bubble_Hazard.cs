using Rewired;
using UnityEngine;

public class Bubble_Hazard : MonoBehaviour
{
    [Tooltip("Use 1-10 for Speed")]
    public float moveSpeed = 0;
    public bool Overlap;
    public bool Overlap2;
    public GameObject Coral;
    public float Meter = 0;
    public GameObject parent;
    private float Transparency = .30f;
    public float Meter2 = 0;
    public Spawner Spawner;
    public float TimeToDeath;
    public float KillY;
    public Animator animatorbubble;

    private void Start()
    {
        //player = PlayerValues.GetPlayer(0).gameObject;//GameObject.FindGameObjectWithTag("Player");
        //player2 = PlayerValues.GetPlayer(1).gameObject;//GameObject.FindGameObjectWithTag("Player2");
        Coral = GameObject.FindGameObjectWithTag("Coral");
        Destroy(gameObject, TimeToDeath);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        PlayerValues playerValues = other.gameObject.GetComponent<PlayerValues>();
        if (playerValues != null)
        {     
            Debug.Log("Bubble bobble");
            if (playerValues.Id == 0)
            {
                playerValues.GetComponent<PlayerMovement>().enabled = false;
                playerValues.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                Overlap = true;
            }
    
            if (playerValues.Id == 1)
            {
                playerValues.GetComponent<PlayerMovement>().enabled = false;
                playerValues.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                Overlap2 = true;
            }
        }

        if (other.tag == "Coral")
        {

            if (Overlap)
            {
                print("Overlap");
                animatorbubble.SetBool("Destroyed", true);
                Meter = 0;
                PlayerValues.GetPlayer(0).GetComponent<PlayerValues>().Die();
                PlayerValues.GetPlayer(0).GetComponent<PlayerMovement>().enabled = true;
            }

            if (Overlap2)
            {
                print("Overlap");
                animatorbubble.SetBool("Destroyed", true);
                Meter2 = 0;
                PlayerValues.GetPlayer(1).GetComponent<PlayerValues>().Die();
                PlayerValues.GetPlayer(1).GetComponent<PlayerMovement>().enabled = true;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Overlap = false;
        }

    }

    public void AnimationEvent()
    {
         Destroy(transform.root.gameObject);
        if (Overlap)
        {
            PlayerValues.GetPlayer(0).GetComponent<PlayerMovement>().enabled = true;
        }

        if (Overlap2)
        {
            PlayerValues.GetPlayer(1).GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void Update()
    {
        print(Meter);
        print(Meter2);
        parent.transform.Translate(0, moveSpeed * Time.deltaTime, 0);

        if (Overlap)
        {
            PlayerValues.GetPlayer(0).transform.position = gameObject.transform.position;
            if (PlayerValues.GetPlayer(0).transform.position.y > KillY)
            {
                Destroy(gameObject);
                PlayerValues.GetPlayer(0).GetComponent<PlayerValues>().Die();
            }
        }


        if (Overlap2)
        {
            PlayerValues.GetPlayer(1).transform.position = gameObject.transform.position;
            if (PlayerValues.GetPlayer(1).transform.position.y > KillY)
            {
                Destroy(gameObject);
                PlayerValues.GetPlayer(1).GetComponent<PlayerValues>().Die();
            }
        }

        if (Overlap)
        {
            if ((ReInput.players.GetPlayer(0).GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
            {
                Meter = Meter + 1;
            }
        }

        if (Overlap2)
        {
            if ((ReInput.players.GetPlayer(1).GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
            {
                Meter2 = Meter2 + 1;
            }
        }

        if (Meter >= 5)
        {
            animatorbubble.SetBool("BubbleHurt", true);
        }

        if (Meter >= 10)
        {
            animatorbubble.SetBool("BubbleHurt", false);
            animatorbubble.SetBool("BubbleBreaking", true);
        }

        if (Meter >= 15)
        {
            animatorbubble.SetBool("BubbleBreaking", false);
            animatorbubble.SetBool("BubbleBroken", true);
        }

        if (Meter2 >= 5)
        {
            animatorbubble.SetBool("BubbleHurt", true);
        }

        if (Meter2 >= 10)
        {
            animatorbubble.SetBool("BubbleHurt", false);
            animatorbubble.SetBool("BubbleBreaking", true);
        }

        if (Meter2 >= 15)
        {
            animatorbubble.SetBool("BubbleBreaking", false);
            animatorbubble.SetBool("BubbleBroken", true);
        }


        if (Meter >= 20)
        {
            animatorbubble.SetBool("Destroyed", true);
            Meter = 0;
            print("You broke free");
        }

        if (Meter2 >= 20)
        {
            animatorbubble.SetBool("Destroyed", true);
            Meter2 = 0;
            print("You broke free");
        }


    }



}
