using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static bool TimerIsRunning;
    private static Timer instance;
    
    public float timeRemaining = 200;
    
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Time.timeScale = 1;
        TimerIsRunning = true;

        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    IEnumerator UpdateTimer()
    {
        while(timeRemaining > 0) {
            yield return new WaitForSeconds(1);
            if (TimerIsRunning)
            {
                timeRemaining -= 1;
                DisplayTime(timeRemaining);
            }
        }

        timeRemaining = 0;
        TimerIsRunning = false;
        //Time.timeScale = 0;
    }

    //displays the timer
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        if(timeToDisplay <= 30) {
            timeText.color = Color.red;
        }

        timeText.text = string.Format("{0:00}: {1:00}", minutes, seconds);
    }

    public static void AddTime(float extraTime) {
        instance.timeRemaining += extraTime;
        instance.DisplayTime(instance.timeRemaining);
    }
}
