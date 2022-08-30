using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CoinScoring : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject scoreText;

    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput();
    }
    
    //when character is tapped, gives an order and sets the character's tag. Possibly to be deleted later
    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //obtain finger position
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            //switch start
            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (_collider == Physics2D.OverlapPoint(touchPos))
                    {
                        GameObject text = Instantiate(scoreText, CanvasInstance.instance.transform);
                        text.GetComponent<RectTransform>().position = touch.position;
                        ScorePreview score = text.GetComponent<ScorePreview>();
                        score.StartPos = touch.position;
                        score.SetText("+" + coinValue);

                        ScoreManager.instance.ScoreValue += coinValue;
                        Destroy(gameObject);
                    }

                    break;

                case TouchPhase.Ended:
                    break;
            }

        }
    }
}

