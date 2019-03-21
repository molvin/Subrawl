using System.Collections;
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
    public float maxWiggle = 0.2f;

    private bool popped;
    public float PlayerPositionSmoothingTime = 0.1f;

    public MinMaxFloat MaxXDelta;
    public MinMaxFloat XSpeed;
    private float xOrigin;
    private float timeAlive;
    private float maxXDelta;
    private float xSpeed;

    private Vector3 ShakeVector;
    
    private void Start()
    {
        //player = PlayerValues.GetPlayer(0).gameObject;//GameObject.FindGameObjectWithTag("Player");
        //player2 = PlayerValues.GetPlayer(1).gameObject;//GameObject.FindGameObjectWithTag("Player2");
        Coral = GameObject.FindGameObjectWithTag("Coral");
        Destroy(transform.root.gameObject, TimeToDeath);
        xOrigin = parent.transform.position.x;
        //random start x
        timeAlive = Random.Range(0.0f, 100.0f);
        xSpeed = XSpeed.GetRandomValue();
        maxXDelta = MaxXDelta.GetRandomValue();
        ShakeVector = new Vector3(Mathf.Clamp(ShakeVector.x, -5, 5), 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Pacman"))
        {   
            Destroy(other.gameObject);
            if(Overlap || Overlap2)
                PopBubble(true);
            else
                PopBubble(false);
            return;
        }
        
        
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
            if (playerValues.Id == 0 && !playerValues.Invincible)
            {
                Overlap = true;
                playerValues.OnDeath += () => PopBubble(true);
                AudioManager.PlaySound("Trap");
            } 
            if (playerValues.Id == 1 && !playerValues.Invincible)
            {
                Overlap2 = true;
                playerValues.OnDeath += () => PopBubble(true);
                AudioManager.PlaySound("Trap");
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
        //Wiggle
        Vector3 position = parent.transform.position;
        position.x = xOrigin + maxXDelta * Mathf.Sin((timeAlive += Time.deltaTime * xSpeed));
        parent.transform.position = position;
        
        if (Overlap && PlayerValues.GetPlayer(0) == null)
            return;
        if (Overlap2 && PlayerValues.GetPlayer(1) == null)
            return;
        
        if (Overlap)
        {
            //PlayerValues.GetPlayer(0).transform.position = gameObject.transform.position;
            Vector3 positionDelta = Vector3.zero;
            PlayerValues.GetPlayer(0).transform.position = Vector3.Lerp(PlayerValues.GetPlayer(0).transform.position, transform.position, 30 * Time.deltaTime);//Vector3.SmoothDamp(PlayerValues.GetPlayer(0).transform.position, transform.position, ref positionDelta, PlayerPositionSmoothingTime, float.MaxValue, Time.deltaTime);
            if (PlayerValues.GetPlayer(0).transform.position.y > KillY)
            {
                PopBubble(true);
                return;
            }
        }
        if (Overlap2)
        {
            //PlayerValues.GetPlayer(1).transform.position = gameObject.transform.position;
            Vector3 positionDelta = Vector3.zero;
            PlayerValues.GetPlayer(1).transform.position = Vector3.Lerp(PlayerValues.GetPlayer(1).transform.position, transform.position, 30 * Time.deltaTime);//Vector3.SmoothDamp(PlayerValues.GetPlayer(1).transform.position, transform.position, ref positionDelta, PlayerPositionSmoothingTime, float.MaxValue, Time.deltaTime);
            if (PlayerValues.GetPlayer(1).transform.position.y > KillY)
            {
                PopBubble(true);
                return;
            }
        }
        
        
        //Breaking bubble
        if (Overlap)
        {
            if ((ReInput.players.GetPlayer(0).GetButtonDown("Struggle1") || ReInput.players.GetPlayer(0).GetButtonDown("Struggle2") || ReInput.players.GetPlayer(0).GetButtonDown("Struggle3") || ReInput.players.GetPlayer(0).GetButtonDown("Struggle4")))
            {
                Meter = Meter + 1;
                PlayerValues.GetPlayer(0).transform.position += (Vector3)Random.insideUnitCircle * maxWiggle;   
            }
        }

        if (Overlap2)
        {
            if ((ReInput.players.GetPlayer(1).GetButtonDown("Struggle1") || ReInput.players.GetPlayer(1).GetButtonDown("Struggle2") || ReInput.players.GetPlayer(1).GetButtonDown("Struggle3") || ReInput.players.GetPlayer(1).GetButtonDown("Struggle4")))
            {
                Meter2 = Meter2 + 1;
                PlayerValues.GetPlayer(1).transform.position += (Vector3)Random.insideUnitCircle * maxWiggle;

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
            AudioManager.PlaySound("Pop");

            animatorbubble.SetBool("Destroyed", true);
            //PopBubble(false);
            Meter = 0;
            print("You broke free");
        }

        if (Meter2 >= 20)
        {
            AudioManager.PlaySound("Pop");

            animatorbubble.SetBool("Destroyed", true);
            //PopBubble(false);
            Meter2 = 0;
            print("You broke free");
        }


    }

    void PopBubble(bool killPlayer)
    {
        if (popped)
            return;
        popped = true;
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
        
        if(animatorbubble != null)
            animatorbubble.SetBool("Destroyed", true);
        //Destroy(this);
        //Destroy(transform.root.gameObject, 5f);
    }
}
