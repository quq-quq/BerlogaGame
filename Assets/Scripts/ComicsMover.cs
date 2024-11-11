using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Save_files.Scripts;
using UnityEngine.UI;

public class ComicsMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform[] _transformPoints;
    [Space(20)]
    [SerializeField] private float _tweenDuration;
    [SerializeField] private float _showTransitTime;
    [SerializeField] private float _hideTransitTime;
    [Space(20)]
    [SerializeField] private Image _image;

    private int _i = 0;
    private Tween _currentTween;
    private bool _isHiding = false;

    void Start()
    {
        _cameraTransform.position = _transformPoints[0].position;
        Show();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && _i < _transformPoints.Length-1)
        {
            StopTween();
            _i++;
            _cameraTransform.DOMove(_transformPoints[_i].position, _tweenDuration);
            _cameraTransform.DORotate(_transformPoints[_i].rotation.eulerAngles, _tweenDuration);
        }
        if (Input.GetKeyDown(KeyCode.A) && _i>0)
        {
            StopTween();
            _i--;
            _cameraTransform.DOMove(_transformPoints[_i].position, _tweenDuration);
            _cameraTransform.DORotate(_transformPoints[_i].rotation.eulerAngles, _tweenDuration);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isHiding)
        {
            Hide();          
        }

    }

    void StopTween()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
            _currentTween = null;
        }
    }

    public void Hide()
    {
        _isHiding = true;
        StartCoroutine(HideCoroutine());
        IEnumerator HideCoroutine()
        {
            var t = _hideTransitTime;
            var tColor = Color.black;
            var a = 0f;

            while (t > 0)
            {
                a = Mathf.Lerp(1, 0, t / _hideTransitTime);
                t -= Time.deltaTime;
                tColor.a = a;
                _image.color = tColor;
                yield return new WaitForEndOfFrame();
            }
            SceneManager.LoadScene("NEW menu");

        }
    }

    public void Show()
    {
        StartCoroutine(ShowCoroutine());
        IEnumerator ShowCoroutine()
        {
            var t = _showTransitTime;
            var tColor = Color.black;
            var a = 1f;

            while (t > 0)
            {
                a = Mathf.Lerp(0, 1, t / _showTransitTime);
                t -= Time.deltaTime;
                tColor.a = a;
                _image.color = tColor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
