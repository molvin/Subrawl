using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        anim.SetBool("BubblesAreSpawning", true);
    }

    public void Hello()
    {
        print("Hello");
    }
}
