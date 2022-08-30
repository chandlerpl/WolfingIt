using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCopies : MonoBehaviour
{
    public GameObject customerPrefab;
    private float respawnTime = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Copies());
    }

    void Customer_Copies()
    {
        GameObject customer = Instantiate(customerPrefab);
        customer.transform.position = new Vector2( 2.67f, 8.0f);
        
    }

    IEnumerator Copies()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            Customer_Copies();
        }

    }
}
