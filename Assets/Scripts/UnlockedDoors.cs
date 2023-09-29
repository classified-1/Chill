using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedDoors : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
            animator.SetTrigger("open");
            Destroy(this);
        }
    }
}
