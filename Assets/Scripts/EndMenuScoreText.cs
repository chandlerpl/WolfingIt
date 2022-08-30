using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuScoreText : MonoBehaviour
{

    private int _starNumber;
    
    public Text scoreText; //score text variable

    public Image[] scoreStars; 
    public Confetti confetti;

    public Sprite completedStar;
    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GetComponent<Text>();
        
    }

    void Awake()
    {
        if (ScoreManager.instance.ScoreValue >= ScoreManager.instance.stars[2].requiredScore)
        {
            scoreStars[2].sprite = completedStar;
            scoreStars[1].sprite = completedStar;
            scoreStars[0].sprite = completedStar;
            scoreText.text = "Well Done! You collected " + ScoreManager.instance.ScoreValue + " coins!";

            StartCoroutine(confetti.PlayFullEffect(4));
        } else if (ScoreManager.instance.ScoreValue >= ScoreManager.instance.stars[1].requiredScore)
        {
            scoreStars[1].sprite = completedStar;
            scoreStars[0].sprite = completedStar;
            
            scoreText.text = "Well Done! You collected " + ScoreManager.instance.ScoreValue + " coins!";
            
        } else if (ScoreManager.instance.ScoreValue >= ScoreManager.instance.stars[0].requiredScore )
        {
            scoreStars[0].sprite = completedStar;
            scoreText.text = "Well Done! You collected " + ScoreManager.instance.ScoreValue + " coins!";
        } 
        
        else
        {
            scoreText.text = "Try again!";
        }
    }
}
