using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FoodCopies : MonoBehaviour
{
    public GameObject [] foodPrefab;
     
    public float respawnTime = 7.0f;
    //creates a new queue to hold orders
   
    


    private void Start()
    {
        StartCoroutine(FoodLine());
    }

    private void CopyFood()
    {
        int foodIndex = Random.Range(0,foodPrefab.Length);
        
        Instantiate(foodPrefab [foodIndex], new Vector2( -1.67f, 3.0f),foodPrefab[foodIndex].transform.rotation);
      //  food.transform.position = new Vector2( -1.67f, 3.0f);
    }

    IEnumerator FoodLine()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            CopyFood();
        }
        
    }
    
    
}
