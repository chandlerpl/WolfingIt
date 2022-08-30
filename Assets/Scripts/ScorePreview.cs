using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePreview : MonoBehaviour
{
    public Vector3 StartPos { get => _startPos; set { _startPos = value; _endPos = value + new Vector3(0, 100); } }
    public Image image;

    private Vector3 _startPos;
    private Vector3 _endPos;
    private Text _text;
    private AudioSource _audioSource;

    private Color _imageStartColor;
    private Color _imageEndColor;
    private Color _startColor;
    private Color _endColor;
    void Start() {
        _startPos = transform.position;
        _endPos = transform.position + new Vector3(0, 100);

        _text = GetComponent<Text>();
        _audioSource = GetComponent<AudioSource>();

        _imageStartColor = image.color;
        _imageEndColor = image.color;
        _imageEndColor.a = 0;
        
        _startColor = _text.color;
        _endColor = _text.color;
        _endColor.a = 0;
    }
    private float _time;

    void Update()
    {
        _time += Time.deltaTime;

        transform.position = Vector2.Lerp(_startPos, _endPos, _time);
        _text.color = Color.Lerp(_startColor, _endColor, _time);
        image.color = Color.Lerp(_imageStartColor, _imageEndColor, _time);

        if(_time > 1) 
            Destroy(gameObject);
    }

    public void SetText(string text, int fontSize = 0) {
        if(_text == null)
            _text = GetComponent<Text>();
        
        if(fontSize > 0)
            _text.fontSize = fontSize;
        _text.text = text;
    }

    public void SetAudioSource(AudioClip clip) {
        if(_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void ToggleAudioSource() {
        if(_audioSource == null)
            _audioSource = GetComponent<AudioSource>();

        _audioSource.enabled = false;
    }

    public void ToggleImage() {
        if(image != null) {
            image.enabled = false;
        }
    }
}
