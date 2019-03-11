using UnityEngine;

public class Bubble_Hazard : MonoBehaviour
{
    [Tooltip("Use 1-10 for Speed")]
    public float moveSpeed = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log ("Bubble bobble");
    }

    private void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
    }
}
