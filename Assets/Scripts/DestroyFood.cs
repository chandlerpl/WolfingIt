using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyFood : MonoBehaviour
{
    
   private Collider2D _binCollider;
    
   private GameObject _plate;

   
   private Collider2D _plateCollider;

   private void Start()
   {
       _binCollider = GetComponent<Collider2D>();
       _plate = GameObject.FindGameObjectWithTag("plate");
       _plateCollider = _plate.GetComponent<Collider2D>();

   }

  
   // Update is called once per frame
    void Update()
    {
        //if the binCollider touches another collider, destroy the other gameObject
        
        if (_binCollider.IsTouching(_plateCollider))
        {
            Destroy(_plate);
            
        }
    }

    
}
