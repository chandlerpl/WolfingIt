using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSViewer : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponent<Text>();

        StartCoroutine(UpdateFPS());
    }

    IEnumerator UpdateFPS() {
        while(true) {
            yield return new WaitForSecondsRealtime(1);

            _text.text = ((int)(1f / Time.smoothDeltaTime)).ToString() + " FPS";
        }
    }
}
