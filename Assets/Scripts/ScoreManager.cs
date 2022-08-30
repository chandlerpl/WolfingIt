    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [System.Serializable]
    public class ScoreStar {
        public Image star;
        public int requiredScore = 1;
        [HideInInspector]
        public bool isCompleted = false;
    }
  //  public int _currentScore;
   
   // public ScoreSliderScript scoreSlider;
    public static ScoreManager instance;

    public int maxScore = 20;
    public ScoreStar[] stars;
    public Sprite completedStar;

    public int ScoreValue { get => _scoreValue; set { _scoreValue = value; UpdateScore(); } }

    private int _scoreValue = 0;
    private Slider _slider;

    public AudioSource audioSourceStar;

   
    

   // private Text _score;
    // Start is called before the first frame update
    void Start()
    { 
        if(instance != null) {
            Destroy(instance.gameObject);
        }
        instance = this;

        ScoreValue = 0;
        // _currentScore = _maxScore;
        _slider = GetComponent<Slider>();
        //  scoreSlider.SetMaxScore(_maxScore);

        audioSourceStar = gameObject.GetComponent<AudioSource>();
    }

    void UpdateScore() {
        if(_slider != null) {
            StartCoroutine(LerpScore(ScoreValue / (float)maxScore));

            foreach(ScoreStar star in stars) {
                if(star.isCompleted)
                    continue;
                if(ScoreValue >= star.requiredScore) {
                    star.isCompleted = true;
                    audioSourceStar.Play();
                    star.star.sprite = completedStar;
                }
            }
        }
    }

    private IEnumerator LerpScore(float endValue) {
        float startValue = _slider.value;

        float t = 0f;
        while(t <= 1) {
            t += 0.1f;
            _slider.value = Mathf.Lerp(startValue, endValue, t);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
