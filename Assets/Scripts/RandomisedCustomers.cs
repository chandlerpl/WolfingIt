
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomisedCustomers : MonoBehaviour
{
    public GameObject customerPrefab;

    //private GameObject _customerTest;
    
    private float _spawnRangeY = 4;

    private float _spawnPosX = 6;

    public float respawnTime = 6;

    private float _customerTimer;

    public Sprite angryCustomer;

    //public float speed;
  

    // Update is called once per frame
    void Start()
    {
        _customerTimer = 10;
       // Vector2 spawnPos = new Vector2(spawnPosX, Random.Range(-spawnRangeY, spawnRangeY));
       StartCoroutine(CustomerCopies());
    
       
    }
    
    
    

    //starts coroutine to make copies of customer
    IEnumerator CustomerCopies()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            CustomerCopies2();
            
        }
    }

    void CustomerCopies2()
    {
        GameObject customerTest= Instantiate(customerPrefab);
        respawnTime = Random.Range(5, 15);
        
        customerTest.transform.position = new Vector2(_spawnPosX, Random.Range(-_spawnRangeY, _spawnRangeY)); 
        
    }

    void CustomerTimer()
    {
        _customerTimer -= Time.deltaTime;

        if (_customerTimer < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = angryCustomer ;
        }


    }
}
