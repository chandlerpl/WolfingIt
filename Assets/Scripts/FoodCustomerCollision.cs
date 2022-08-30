using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class FoodCustomerCollision : MonoBehaviour
{
    public int orderIndex;
    public int orderValue = 1;

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag.Equals("bin")) 
            Destroy(gameObject);


        if (gameObject.tag.Contains("plate") && other.gameObject.tag.Contains("customer"))
        {
            if(other.gameObject.GetComponent<CustomerOrder>().OrderComplete(orderIndex, orderValue))
            {
                //ScoreManager.instance.ScoreValue++;
                Destroy(gameObject);
            }
        }
    }
}
