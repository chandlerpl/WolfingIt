using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPatienceTimer : MonoBehaviour
{
    public static float CustomerTimer = 30f;
    private float _customerTimer;
    public GameObject [] patienceHearts;

    public Sprite angryCustomer;


    private void OnEnable()
    {
        _customerTimer = CustomerTimer;

        StartCoroutine(RunTimer());
    }

    public void AddTime(float time) {
        _customerTimer += time;
    }

    //reduces the customerTimer patience by 0.1
    IEnumerator RunTimer() {
        while(_customerTimer > 1 && Timer.TimerIsRunning) {
            yield return new WaitForSeconds(0.1f);
            _customerTimer -= 0.1f;

            //if the customer patience is over 31, everything is set true
            if (_customerTimer >=23f){
                patienceHearts[0].SetActive(true);
                patienceHearts[1].SetActive(true);
                patienceHearts[2].SetActive(true);
            }
            else if (_customerTimer <= 22f && _customerTimer >= 15f)
            {
                patienceHearts[0].SetActive(false);
                patienceHearts[1].SetActive(true);
                patienceHearts[2].SetActive(true);
            }
            else if (_customerTimer <= 14f)
            {
                
                patienceHearts[0].SetActive(false);
                patienceHearts[1].SetActive(false);
                patienceHearts[2].SetActive(true);
                

            }
            
            
           
            
        }

        //set the last heart to false once the while loop is finished
        patienceHearts[2].SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = angryCustomer;
        StartCoroutine(GetComponent<CustomerOrder>().RemoveCustomer(false));
    }
}
