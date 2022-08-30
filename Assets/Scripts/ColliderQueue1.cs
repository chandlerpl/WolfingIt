using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderQueue1 : MonoBehaviour
{
  public GameObject[] queueColliders;
  

  private int touching = 0;
  private void Start()
  {
    //sets the queue colliders as false 
    queueColliders[1].SetActive(false);
    queueColliders[2].SetActive(false);
    queueColliders[3].SetActive(false);
    queueColliders[4].SetActive(false);
    queueColliders[5].SetActive(false);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.gameObject.tag.Contains("customer") || other.gameObject.tag.Contains("coins")) //other.gameObject.CompareTag("customerTest") || 
    {
      queueColliders[1].SetActive(true);
      ++touching;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.gameObject.tag.Contains("customer") || other.gameObject.tag.Contains("coins"))
    {
      if(--touching <= 0) {
        touching = 0;
        queueColliders[1].SetActive(false);
        queueColliders[2].SetActive(false);
        queueColliders[3].SetActive(false);
        queueColliders[4].SetActive(false);
        queueColliders[5].SetActive(false);
      }
    }
  }
  


}
