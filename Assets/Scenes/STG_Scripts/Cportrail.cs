using UnityEngine;

public class Cportrail : MonoBehaviour
{
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void trueAnimation()
    {
        animator.SetFloat("T", 1f);
    }
    public void falseAnimation()
    {
        animator.SetFloat("T", 2f);
    }
}
