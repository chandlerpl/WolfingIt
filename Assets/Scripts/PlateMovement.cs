using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateMovement : MonoBehaviour
{
    private Vector3 position;
    //private float width;
    //private float height;
    public Collider2D objectCollider;
    public Collider2D anotherCollider;
   // public Collider2D conveyorCollider;
    

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch =Input.GetTouch(0);
            
            //moves the touched item 
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0f;
                transform.position= touchPosition;
            }

            //checks if the 
            if (touch.phase == TouchPhase.Ended)
            {
                if (objectCollider.IsTouching(anotherCollider))
                {
                    gameObject.SetActive(false);
                    //transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                }
                
            }

           
        }

       
        
    }
}
