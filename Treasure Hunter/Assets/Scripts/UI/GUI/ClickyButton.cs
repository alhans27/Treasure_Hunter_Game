using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;
    public AudioManager audioManager;

    private void Awake() {
    }

    private void Start() {
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        if (gameObject.name == "Audio")
        {
            Debug.Log(gameObject.name);
            if (AudioManager.audioMute == true){
                _img.sprite = _pressed;
               _source.mute = true;
            } else {
                _img.sprite = _default;
               _source.mute = false;
            }
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (AudioManager.audioMute == false)
        {
            _source.PlayOneShot(_compressClip);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GameObject.Find("Audio"))
        {        
            if (GameObject.Find("Audio").GetComponent<ClickyButton>()._img.sprite == _pressed){
                _img.sprite = _default;
                _source.mute = !_source.mute;
                audioManager.SetAudio(_source.mute);
            } else if (GameObject.Find("Audio").GetComponent<ClickyButton>()._img.sprite == _default){
                _img.sprite = _pressed;
                _source.mute = !_source.mute;
                audioManager.SetAudio(_source.mute);
            }
        }
    }

    public void IWasClicked()
    {
        // Debug.Log("Clicked");
    }
}
