using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    /* Script gets gameObject moving and determines if the customer should move if it touches the queue or leaves it. */
    
    //Moves customer prefab to the left 
    public float speed;
   // private float _positionX = 8f;
    private bool _collisionDetected;
    private SpriteRenderer _renderer;

   // private float _internalTimer;

   // private Rigidbody2D _rigidBody; 

    // Update is called once per frame
     private void Start()
    {
        //gets customer rigidBody
      //  _rigidBody = GetComponent<Rigidbody2D>();
        //sets collision detection to false
        _collisionDetected = false;
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!Timer.TimerIsRunning)
            return;
        //if the gameobject's x position is greater than the current gameobject position move and if collision is not detected
        if (gameObject.transform.position.x > -0.2f && _collisionDetected == false) 
        {
            gameObject.transform.Translate(Vector3.left * (Time.deltaTime * speed)); //changed vector3.left to right, return to normal 
            _renderer.flipX = false;

        }
        else if(gameObject.transform.position.x < -0.2f && _collisionDetected == false)
        {
            gameObject.transform.Translate(Vector3.right * (Time.deltaTime * speed));
            _renderer.flipX = true;
            
        }

        else
        {
            gameObject.transform.Translate(0,0,0);
        }
        
        
    }

    
    //checks if the customer is touching the queue
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("queue"))
        {
            _collisionDetected = true;
        }
    }

   // is this needed? hmmm
     private void OnTriggerExit2D(Collider2D other)
     {
         if (other.gameObject.tag.Contains("queue"))
         {
             _collisionDetected = false;
         }
     }
}
