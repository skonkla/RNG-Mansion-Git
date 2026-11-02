using UnityEngine;

public class Transition : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToBlack()
    {
        anim.SetTrigger("Fade");
    }
}
