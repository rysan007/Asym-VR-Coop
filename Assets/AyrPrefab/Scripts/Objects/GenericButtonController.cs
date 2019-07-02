using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericButtonController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimateButtonPressed()
    {
        animator.SetBool("buttonPressed", true);
    }
    public void AnimateButtonUnpressed()
    {
        animator.SetBool("buttonPressed", false);
    }
}
