using UnityEngine;

public class Bubble_Hazard : MonoBehaviour
{
    [Tooltip("Use 1-10 for Speed")]
    public float moveSpeed = 0;
    public GameObject player;
    public GameObject player2;
    public bool Overlap;
    public bool Overlap2;
    public GameObject Coral;
    public float Meter = 0;
    public GameObject parent;
    private float Transparency = .30f;
    public float Meter2 = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        Coral = GameObject.FindGameObjectWithTag("Coral");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Overlap = true;
            Debug.Log("Bubble bobble");
            player.GetComponent<PlayerMovement>().enabled = false;
            player.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }

        if (other.tag == "Player2")
        {
            Overlap2 = true;
            Debug.Log("Bubble bobble");
            player2.GetComponent<PlayerMovement>().enabled = false;
            player2.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        }

        if (other.tag == "KillZone")
        {
            Destroy(transform.root.gameObject);

            if (Overlap)
            {
                Meter = 0;
                player.GetComponent<PlayerValues>().Die();
                player.GetComponent<PlayerMovement>().enabled = true;
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

    void Update()
    {
        parent.transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        print(Transparency);

        if (Overlap)
        {
            player.transform.position = gameObject.transform.position;
        }


        if (Overlap2)
        {
            player2.transform.position = gameObject.transform.position;
        }

        if (Overlap)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Meter = Meter + 1;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Transparency - .1f);
            }
        }

        if (Overlap2)
        {
            if (Input.GetButtonDown("Submit"))
            {
                Meter2 = Meter2 + 1;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Transparency - .1f);
            }
        }

        print(Meter2);

        if (Meter >= 30)
        {
            Destroy(transform.root.gameObject);
            player.GetComponent<PlayerMovement>().enabled = true;
            Meter = 0;
            print("You broke free");
        }

        if (Meter2 >= 30)
        {
            Destroy(transform.root.gameObject);
            player2.GetComponent<PlayerMovement>().enabled = true;
            Meter2 = 0;
            print("You broke free");
        }


    }



}
