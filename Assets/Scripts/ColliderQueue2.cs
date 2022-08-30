using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderQueue2 : MonoBehaviour
{
    public GameObject queueColliders;

    public GameObject queueCollidersExit;

    private int touching = 0;
    private void OnTriggerEnter2D(Collider2D other) //checks if the other object is a plate
    {
        
        if (other.gameObject.tag.Contains("customer") || other.gameObject.tag.Contains("coins"))
        {
            queueColliders.SetActive(true);
            ++touching;
           
        }
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("customer") || other.gameObject.tag.Contains("coins"))
        {
            if(--touching <= 0) {
                touching = 0;
                queueCollidersExit.SetActive(false);
            }
        }
    }

}
