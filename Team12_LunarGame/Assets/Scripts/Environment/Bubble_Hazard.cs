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
        Destroy(transform.root.gameObject, TimeToDeath);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        PlayerValues playerValues = other.gameObject.GetComponent<PlayerValues>();

        
        
        
        if (playerValues != null)
        {     
            if (Overlap && playerValues.Id == 1)
            {
                PopBubble(true);
                return;
            }

            if (Overlap2 && playerValues.Id == 0)
            {
                PopBubble(true);
                return;
            }
            Debug.Log("Bubble bobble");
            playerValues.GetComponent<PlayerMovement>().enabled = false;
            if (playerValues.Id == 0)
            {
                Overlap = true;
                playerValues.OnDeath += () => PopBubble(true);
            } 
            if (playerValues.Id == 1)
            {
                Overlap2 = true;
                playerValues.OnDeath += () => PopBubble(true);
            }
        }

        if (other.tag == "Coral")
        {

            if (Overlap)
            {
                PopBubble(true);
//                print("Overlap");
//                animatorbubble.SetBool("Destroyed", true);
//                Meter = 0;
//                if (PlayerValues.GetPlayer(0) != null)
//                {
//                    
//                    PlayerValues.GetPlayer(0).GetComponent<PlayerValues>().Die();
//                    PlayerValues.GetPlayer(0).GetComponent<PlayerMovement>().enabled = true;
//                }

            }

            if (Overlap2)
            {
                PopBubble(true);
//                print("Overlap");
//                animatorbubble.SetBool("Destroyed", true);
//                Meter2 = 0;
//                if (PlayerValues.GetPlayer(1) != null)
//                {
//                    PlayerValues.GetPlayer(1).GetComponent<PlayerValues>().Die();
//                    PlayerValues.GetPlayer(1).GetComponent<PlayerMovement>().enabled = true;
//                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
//        if (other.tag == "Player")
//        {
//            Overlap = false;
//        }

    }

    public void AnimationEvent()
    {
        Destroy(transform.root.gameObject);
        if (Overlap && PlayerValues.GetPlayer(0) != null)
        {
            PlayerValues.GetPlayer(0).GetComponent<PlayerMovement>().enabled = true;
        }

        if (Overlap2 && PlayerValues.GetPlayer(1) != null)
        {
            PlayerValues.GetPlayer(1).GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void Update()
    {
        
        //Movement
        parent.transform.Translate(0, moveSpeed * Time.deltaTime, 0);

        if (Overlap && PlayerValues.GetPlayer(0) == null)
            return;
        if (Overlap2 && PlayerValues.GetPlayer(1) == null)
            return;
        
        if (Overlap)
        {
            PlayerValues.GetPlayer(0).transform.position = gameObject.transform.position;
            if (PlayerValues.GetPlayer(0).transform.position.y > KillY)
            {
                PopBubble(true);
                return;
            }
        }
        if (Overlap2)
        {
            PlayerValues.GetPlayer(1).transform.position = gameObject.transform.position;
            if (PlayerValues.GetPlayer(1).transform.position.y > KillY)
            {
                PopBubble(true);
                return;
            }
        }
        
        
        //Breaking bubble
        if (Overlap)
        {
            if ((ReInput.players.GetPlayer(0).GetButtonDown("Horizontal") || (ReInput.players.GetPlayer(0).GetButtonDown("Vertical"))))
            {
                Meter = Meter + 1;
            }
        }

        if (Overlap2)
        {
            if ((ReInput.players.GetPlayer(1).GetButtonDown("Horizontal") || (ReInput.players.GetPlayer(1).GetButtonDown("Vertical"))))
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
            //PopBubble(false);
            Meter = 0;
            print("You broke free");
        }

        if (Meter2 >= 20)
        {
            animatorbubble.SetBool("Destroyed", true);
            //PopBubble(false);
            Meter2 = 0;
            print("You broke free");
        }


    }

    void PopBubble(bool killPlayer)
    {
        if (killPlayer)
        {
            if (Overlap)
            {
                PlayerValues.GetPlayer(0)?.Die();
            }
            if (Overlap2)
            {
                PlayerValues.GetPlayer(1)?.Die();
            }
        }
        else
        {
            if (Overlap && PlayerValues.GetPlayer(0) != null)
            {
                PlayerValues.GetPlayer(0).GetComponent<PlayerMovement>().enabled = true;
            }
            if (Overlap2 && PlayerValues.GetPlayer(1) != null)
            {
                PlayerValues.GetPlayer(1).GetComponent<PlayerMovement>().enabled = true;
            }
        }     
        
        animatorbubble.SetBool("Destroyed", true);
        //Destroy(this);
        //Destroy(transform.root.gameObject, 5f);
    }
}
