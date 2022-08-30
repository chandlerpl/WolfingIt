using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Select : MonoBehaviour
{

    private bool _touching;
    private Collider2D _levelSelectCollider;
   //Selects Level 1 when user presses

   private void Start()
   {
       _levelSelectCollider = GetComponent<Collider2D>();
   }

   private void Update()
   {
       if (Input.touchCount > 0)
       {
           Touch touch = Input.GetTouch(0);

           //obtain finger position
           Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

           switch (touch.phase)
           {
               case TouchPhase.Began:
                   if (_levelSelectCollider == Physics2D.OverlapPoint(touchPos))
                   {
                       _touching = true;
                   }
               
                   break;
               case TouchPhase.Ended:
                   if (_levelSelectCollider == Physics2D.OverlapPoint(touchPos) && _touching)
                   {
                       SceneManager.LoadScene(1);
                   }

                   break;
           }
       }
   }
}

