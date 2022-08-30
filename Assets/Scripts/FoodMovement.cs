using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class FoodMovement : MonoBehaviour
{
    //reference to Rigidbody2D component
    private Rigidbody2D _rb;

    //reference to the bin game object
    private GameObject _bin;

    //reference to the customer came object
    private GameObject _customer;

    //movement not allowed if you don't touch the ball
    private bool _moveAllowed;

    //reference to the plate and the bin colliders
    private Collider2D _plateCollider;
    private Collider2D _binCollider;
    private Collider2D _conveyorCollider;
    
    //reference to the customer collider
    private Collider2D _customerCollider;
    
    //public GameObject foodCollider;
    private GameObject _test;

    
    
    //public  scoreSlider;

    

    private int _score;
    private Vector3 _startPosition;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _plateCollider = GetComponent<Collider2D>();

        //gets other object's colliders
        _bin = GameObject.FindGameObjectWithTag("bin");
        _binCollider = _bin.GetComponent<Collider2D>();

        
        GameObject conveyor = GameObject.FindGameObjectWithTag("conveyorBelt");
        _conveyorCollider = conveyor.GetComponent<Collider2D>();
        
    }


    void Update()
    {
        if(!Timer.TimerIsRunning) {
            _rb.freezeRotation = true;
            _rb.gravityScale = 0;
            return;
        }

        //initiates touch event
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //obtain finger position
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //checks what touch phase we're in
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    //if you touch the gameObject

                    if (_plateCollider == Physics2D.OverlapPoint(touchPos, LayerMask.GetMask("FoodLayer")))
                    {
                        // foodCollider.SetActive(false);
                        _moveAllowed = true;
                        
                        _startPosition = _plateCollider.transform.position;
                        //restricts some rigidbody properties
                        _rb.freezeRotation = true;
                        _rb.gravityScale = 0;
                    }

                    break;

                case TouchPhase.Moved:
                    //if movement is allowed
                    if (_moveAllowed)
                    {
                        _rb.MovePosition(new Vector3(touchPos.x, touchPos.y,0));
                        
                    }


                    break;

                //when the touch ends, destroy the object
                case TouchPhase.Ended:
                    
                    if(_moveAllowed) {
                        
                        if (_plateCollider.IsTouching(_binCollider))
                        {
                            StartCoroutine(DestroyFood());
                        }

                        else {
                            //food returns back to its original position
                            if(_plateCollider.IsTouching(_conveyorCollider)) {
                                _plateCollider.transform.position = new Vector2(_conveyorCollider.transform.position.x, _plateCollider.transform.position.y);
                            } else {
                                _plateCollider.transform.position = _startPosition;
                            }

                            _moveAllowed = false;
                            //Reapply rigidbody properties
                            _rb.freezeRotation = false;
                            _rb.gravityScale = 0.05f;
                        }
                    }

                    break;
            }
        }
    
        //checks if the plate collider is touching the bin outside of mouse holding
        if (_plateCollider.IsTouching(_binCollider))
        {
            StartCoroutine(DestroyFood());
        }

    }

   
//Destroys the plate Instance upon touching the bin
IEnumerator DestroyFood()
    {
        _plateCollider.enabled = false;
        yield return new WaitForSeconds(0.7f);

        Destroy(gameObject);
    }




}
