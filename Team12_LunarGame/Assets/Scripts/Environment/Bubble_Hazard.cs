using UnityEngine;

public class Bubble_Hazard : MonoBehaviour
{
    [Tooltip("Use 1-10 for Speed")]
    public float moveSpeed = 0;
    public GameObject player;
    public bool Overlap;
    public GameObject Coral;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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

        if (other.tag == "Coral")
        {
            Destroy(this.gameObject);

            if (Overlap)
            {
                Destroy(player.gameObject);
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
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        print(transform.position);

        if (Overlap)
        {
            player.transform.position = gameObject.transform.position;
        }

    }


}
