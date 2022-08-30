using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerMovement : MonoBehaviour
{
    public int speed;

    public float timeToWait;
    // Start is called before the first frame update
  

    private void Update()
    {
        StartCoroutine(WaitingTime());
        
        if (gameObject.transform.position.x < (-5.0f))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    IEnumerator WaitingTime()
    {
        yield return new WaitForSeconds(timeToWait);
        gameObject.transform.Translate(Vector3.left * (Time.deltaTime * speed));
    }
}
