using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer renderer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        renderer.enabled = false;
    }
    public void PlayAnim()
    {
        anim.SetBool("BubblesAreSpawning", true);
        renderer.enabled = true;
    }

    public void StopAnim()
    {
        anim.SetBool("BubblesAreSpawning", false);
        GetComponent<SpriteRenderer>().enabled = false;
        renderer.enabled = false;
    }

    public void Hello()
    {
        print("Hello");
    }
}
