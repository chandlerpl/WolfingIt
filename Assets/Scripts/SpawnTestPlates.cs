using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnTestPlates : MonoBehaviour
{
   
   public GameObject [] foodPrefab;
   public Text timerPlate1;
   private float _timerTimePlate1;
   public Text timerPlate2;
   private float _timerTimePlate2;
   public Text timerPlate3;
   private float _timerTimePlate3;
   public Text timerPlate4;
   private float _timerTimePlate4;
   private bool _plate1Spawned;
   private bool _plate2Spawned;
   private bool _plate3Spawned;
   private bool _plate4Spawned;

   private float _intTimerPlate1;
   private float _intTimerPlate2;
   private float _intTimerPlate3;
   private float _intTimerPlate4;

   private float _x;
   private float _y;


   private void Start()
   {
      _timerTimePlate1 = 1;
      _timerTimePlate2 = 5;
      _timerTimePlate3 = 3;
      _timerTimePlate4 = 7;

      _x = 0.0f;
      _y = 2.0f;
   }


   private void Update()
   {
      if (_plate1Spawned)
      {
         _timerTimePlate1 -= Time.deltaTime;

         _intTimerPlate1 = Mathf.RoundToInt(_timerTimePlate1);
      
          timerPlate1.text = _intTimerPlate1.ToString(); //string.Format("{0:00}: {1:00}", minutes, seconds);

          
         if (_timerTimePlate1 < 0)
         {
            _plate1Spawned = false;
            _timerTimePlate1 = 1;
          
            
          timerPlate1.text = _timerTimePlate1.ToString();//string.Format("{0:00}: {1:00}", minutes, seconds);
        
         }  
      }
      //plate 2 spawned
      if (_plate2Spawned)
      {
         _timerTimePlate2 -= Time.deltaTime;
         _intTimerPlate2 = Mathf.RoundToInt(_timerTimePlate2);
         
         timerPlate2.text = _intTimerPlate2.ToString();//string.Format("{0:00}: {1:00}", minutes, seconds);

          
         if (_timerTimePlate2 < 0)
         {
            _plate2Spawned = false;
            _timerTimePlate2 = 5;
            
            

            timerPlate2.text = _timerTimePlate2.ToString(); //string.Format("{0:00}: {1:00}", minutes, seconds);

         }  
      }
      //plate 3 spawned
      if (_plate3Spawned)
      {
         _timerTimePlate3 -= Time.deltaTime;
         _intTimerPlate3 = Mathf.RoundToInt(_timerTimePlate3);
         
       

         timerPlate3.text = _intTimerPlate3.ToString(); //string.Format("{0:00}: {1:00}", minutes, seconds);

          
         if (_timerTimePlate3 < 0)
         {
            _plate3Spawned = false;
            _timerTimePlate3 = 3;
            
            
            timerPlate3.text = _timerTimePlate3.ToString(); // string.Format("{0:00}: {1:00}", minutes, seconds);

         }  
      }
      
      //plate 4 spawned
      
      if (_plate4Spawned)
      {
         _timerTimePlate4 -= Time.deltaTime;
         _intTimerPlate4 = Mathf.RoundToInt(_timerTimePlate4);
         
         
         timerPlate4.text = _intTimerPlate4.ToString(); //string.Format("{0:00}: {1:00}", minutes, seconds);

          
         if (_timerTimePlate4 < 0)
         {
            _plate4Spawned = false;
            _timerTimePlate4 = 7;
            
           

            timerPlate4.text = _timerTimePlate4.ToString();  //string.Format("{0:00}: {1:00}", minutes, seconds);

         }  
      }
   }

   public void SpawnPlate1()
   {
      if (_plate1Spawned == false)
      {
         StartCoroutine(SpawnPlate1Delayed());
         _plate1Spawned = true;
      }


   }
   public void SpawnPlate2()
   {
      if (_plate2Spawned == false)
      {
         StartCoroutine(SpawnPlate2Delayed());
         _plate2Spawned = true;

      }
   }
   public void SpawnPlate3()
   {
      if (_plate3Spawned == false)
      {
         StartCoroutine(SpawnPlate3Delayed());
         _plate3Spawned = true;
      }

     
   }
   public void SpawnPlate4()
   {
      if (_plate4Spawned == false)
      {
         StartCoroutine(SpawnPlate4Delayed());
         _plate4Spawned = true;
      }
   }


   IEnumerator SpawnPlate1Delayed()
   {
      yield return new WaitForSeconds(1);
      Instantiate(foodPrefab [0], new Vector2( _x, _y),foodPrefab[0].transform.rotation);
      

   }
   IEnumerator SpawnPlate2Delayed()
   {
      yield return new WaitForSeconds(5);
      Instantiate(foodPrefab [1], new Vector2( _x, _y),foodPrefab[0].transform.rotation);
      
   }
   IEnumerator SpawnPlate3Delayed()
   {
      yield return new WaitForSeconds(3);
      Instantiate(foodPrefab [2], new Vector2( _x, _y),foodPrefab[0].transform.rotation);
      
   }
   IEnumerator SpawnPlate4Delayed()
   {
      yield return new WaitForSeconds(7);
      Instantiate(foodPrefab [3], new Vector2( _x, _y),foodPrefab[0].transform.rotation);
      
   }
   
  
}
