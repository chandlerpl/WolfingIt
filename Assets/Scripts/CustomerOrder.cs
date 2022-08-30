
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomerOrder : MonoBehaviour
{
    public float addPatienceTime = 50f;

    public AudioSource audioSrc;

    public Sprite[] orderCustomer;
    public Texture[] orderSprite;
    public Sprite normalCustomer;
    public Sprite angryCustomer;
    public Sprite happyCustomer;
    
    public ParticleSystem particles;

    public GameObject coin;
    //determines if the tag has been set
    //private bool _tagSet;
   // private bool _testtime;

    [Header("Double Order Settings")]
    [Range(0, 1)]
    public float doubleOrderChance = 0.2f;
    [Tooltip("The amount of time the customer is willing to wait longer than usual for the second item.")]
    public float addPatienceOnDelivery = 50f;
    public Sprite[] doubleOrderCustomer;

    private int _customerOrderIndex;
    private SpriteRenderer _spriteRenderer;
    private CustomerPatienceTimer _timer;
    private Movement _movement;

    // Double order variables
    private bool _isDoubleOrder = false;
    private int _customerOrderIndex2;

    private int customerValue = 0;
    private bool delivered = false;
    private void Start()
    {
        _movement = gameObject.GetComponent<Movement>();
        _timer = gameObject.GetComponent<CustomerPatienceTimer>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>(); //gets the spriterenderer component of the gameObject
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        //_tagSet = false; //Resets the tag to false for the customer
        _customerOrderIndex = Random.Range(1, orderCustomer.Length - 1); //initialises random index value
        customerValue = 0;
        delivered = false;

        if(doubleOrderChance > 0) {
            _isDoubleOrder = Random.Range(0f, 1f) < doubleOrderChance;
        }
        if(_isDoubleOrder) {
            _customerOrderIndex2 = Random.Range(1, orderCustomer.Length - 1); // Sets the random value for the second order item

            // Orders the food indices so that the second one is always higher than the first, this is required for the equation to set the sprite to work.
            if(_customerOrderIndex2 < _customerOrderIndex) {
                int temp = _customerOrderIndex2;
                _customerOrderIndex2 = _customerOrderIndex;
                _customerOrderIndex = temp;
            }
        }

        if(_movement == null)
            _movement = gameObject.GetComponent<Movement>();
        _movement.enabled = true;
        GetComponent<Collider2D>().enabled = true;
        if(_spriteRenderer == null) 
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = normalCustomer;
    }
    
    public bool OrderComplete(int orderIndex, int orderValue) {
        if(_isDoubleOrder) {
            if(orderIndex == _customerOrderIndex || orderIndex == _customerOrderIndex2) { // Checks if the food that has been passed to the chicken matches one of the orders
                particles.GetComponent<ParticleSystemRenderer>().material.mainTexture = orderSprite[orderIndex - 1];
                particles.Play(); // Plays the particles for the food delivered
                
                //audio clip for delivered food goes here
                audioSrc.Play();
                
                this.customerValue += orderValue;

                _isDoubleOrder = false; // removes the fact it is a double order
                if(orderIndex == _customerOrderIndex)
                    _customerOrderIndex = _customerOrderIndex2; // Sets the remaining orderIndex to the correct value.
                
                //audio clip for delivered food goes here
                audioSrc.Play();
                
                _timer.AddTime(addPatienceOnDelivery);
                _spriteRenderer.sprite = orderCustomer[_customerOrderIndex];
                return true;
            }
        } else if(!delivered && orderIndex == _customerOrderIndex) {
            _movement.enabled = false; // Stops the movement so that it doesn't walk into the conveyor during its end
            particles.GetComponent<ParticleSystemRenderer>().material.mainTexture = orderSprite[orderIndex - 1];
            particles.Play(); // Plays the particles for the food delivered
            
            
            //audio clip for delivered food goes here
            audioSrc.Play();
            this.customerValue += orderValue;

            _spriteRenderer.sprite = happyCustomer;
            StartCoroutine(RemoveCustomer(true));
            delivered = true;

            return true;
        }

        return false;
    }

    //coroutine to remove customer and push it back to the pool
    public IEnumerator RemoveCustomer(bool spawnCoin) {
        yield return new WaitForSeconds(1);

        if(spawnCoin) {
            GameObject obj = Instantiate(coin, transform.position + new Vector3((_spriteRenderer.flipX ? 0.2f : -0.2f), 0), transform.rotation); //makes a coin spawn at the gameobject's last location
            obj.GetComponent<CoinScoring>().coinValue = customerValue;
        }

        ObjectPool.FindPool("Customers").Push(gameObject);
        --SpawnController.Customers; // Lowers the tracker for number of customers on screen
    }
    

 //checks and gives the customer a single or double order sprite
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("customerOrderPlacer"))
        {
            if(_isDoubleOrder) {
                // This is the math required to choose the correct double order sprite.
                int t = orderCustomer.Length - 2;
                int p = _customerOrderIndex < 3 ? 0 : _customerOrderIndex == 3 ? 1 : 3 * (_customerOrderIndex - 3);
                int index = (t * (_customerOrderIndex - 1)) + (_customerOrderIndex2 - _customerOrderIndex) - p;

                gameObject.GetComponent<SpriteRenderer>().sprite = doubleOrderCustomer[index]; //sets the sprite
            } else {
                gameObject.GetComponent<SpriteRenderer>().sprite = orderCustomer[_customerOrderIndex]; //sets the sprite
            }
        }
    }

   
    
    }

  
    
    

