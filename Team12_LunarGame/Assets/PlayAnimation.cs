using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void PlayAnim()
    {
        anim.SetBool("BubblesAreSpawning", true);
    }

    public void StopAnim()
    {
        anim.SetBool("BubblesAreSpawning", false);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Hello()
    {
        print("Hello");
    }
}
