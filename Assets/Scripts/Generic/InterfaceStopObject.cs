using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceStopObject : MonoBehaviour
{
    Animator animator;
    public int speedAnimation;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StopAnimation()
    {
        animator.speed = 0;
    }

    public void ContinueAnimation()
    {
        animator.speed = speedAnimation;
    }

    public void ReverseAnimation()
    {
        animator.speed = -speedAnimation;
    }
}
