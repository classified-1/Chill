using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

   public void Dead()
    {
        animator.SetInteger("AnimState", 3);
        Debug.Log("PLAYED");
    }
}
