using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusTimer : MonoBehaviour
{
    public float extraTime = 30;
    public GameObject scorePreview;
    public AudioClip audioClip;

    //adds time to the main timer as a special bonus
    void Update()
    {
        Vector3 touchPos;
        bool activate = false;

        if(Input.GetMouseButtonDown(0)) {
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activate = true;

        } else if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            //obtain finger position
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            //switch start
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    activate = true;
                    break;

                case TouchPhase.Ended:
                    break;
            }
        } else {
            return;
        }

        if(activate) {
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                Timer.AddTime(extraTime);

                GameObject text = Instantiate(scorePreview, CanvasInstance.instance.transform);
                RectTransform rect = text.GetComponent<RectTransform>();
                rect.position = Input.mousePosition;
                rect.sizeDelta = new Vector2(172, rect.sizeDelta.y);
                ScorePreview score = text.GetComponent<ScorePreview>();
                score.StartPos = Input.mousePosition;
                score.SetText("+" + extraTime + " seconds", 30);
                score.SetAudioSource(audioClip);
                score.ToggleImage();

                Destroy(gameObject);
            }
        }
    }
}
