
using UnityEngine;
using UnityEngine.UI;

public class ScoreSliderScript : MonoBehaviour
{
   public Slider slider;
   public Gradient gradient;
   public Image fill;

   //sets the maximum amount of score
   public void SetMaxScore(int score)
   {
      slider.maxValue = score;
      slider.value = score;
      fill.color = gradient.Evaluate(1f);
   }
   
   //sets the starting score
   public void SetScore(int score)
   {
      slider.value = score;
      fill.color = gradient.Evaluate(slider.normalizedValue);
   }
}
