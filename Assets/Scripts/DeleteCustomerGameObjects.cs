using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCustomerGameObjects : MonoBehaviour
{
    //not sure what this does lmao. where did i use this?
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("customerTest"))
        {
            ObjectPool.FindPool("Customers").Push(other.gameObject);
            SpawnController.Customers--;
        }
    }
}
