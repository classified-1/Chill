using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnOff : MonoBehaviour
{
    public GameObject gameObject;
    public bool On;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (On)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (On)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
