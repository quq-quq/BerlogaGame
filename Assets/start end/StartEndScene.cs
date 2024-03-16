using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartEndScene : MonoBehaviour
{
    [SerializeField] private string _nameNextScene;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private float _transitTime;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TriggerZone _zone;
    [SerializeField] private AudioClip _endAudio;
    [SerializeField] private float _volume;

    private void Awake()
    {
        _canvas.SetActive(true);
        _image.color = Color.black;
        Show();
        _zone.TriggerEnter += OnZoneEnter;
    }
    
    private void OnZoneEnter(Collider2D other)
    {
        if(other.TryGetComponent(out PlayerController p))
        {
            p.enabled = false;
            Hide();
        }
    }
    
    public void Hide()
    {
        StartCoroutine(HideCoroutine());
        SoundController.sounder.SetSound(_endAudio, false, "BackGroundMusic", _volume);
        IEnumerator HideCoroutine()
        {
            var t = _transitTime;
            var tColor = Color.black;
            var a = 0f;

            while (t > 0)
            {
                a = Mathf.Lerp(1, 0, t / _transitTime);
                t -= Time.deltaTime;
                tColor.a = a;
                _image.color = tColor;
                yield return new WaitForEndOfFrame();
            }
            LevelSwitcher.SwitchScene(_nameNextScene);
        }
    }

    public void Show()
    {
        StartCoroutine(ShowCoroutine());
        IEnumerator ShowCoroutine()
        {
            var t = _transitTime;
            var tColor = Color.black;
            var a = 1f;

            while (t > 0)
            {
                a = Mathf.Lerp(0, 1, t / _transitTime);
                t -= Time.deltaTime;
                tColor.a = a;
                _image.color = tColor;
                yield return new WaitForEndOfFrame();
            }
            /////
             t = _transitTime/2;
             tColor = _text.color;
             a = 1f;

            while (t >= 0)
            {
                a = Mathf.Lerp(0, 1, t / (_transitTime / 2));
                t -= Time.deltaTime;
                tColor.a = a;
                _text.color = tColor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
