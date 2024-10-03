using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Save_files.Scripts;
using UnityEngine.UI;

public class ComicsMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform[] _transformPoints;
    [Space(20)]
    [SerializeField] private float _duration;
    [SerializeField] private float _transitTime;
    [Space(20)]
    [SerializeField] private Image _image;

    private int _i = 0;
    private Tween _currentTween;

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
            _cameraTransform.DOMove(_transformPoints[_i].position, _duration);
            _cameraTransform.DORotate(_transformPoints[_i].rotation.eulerAngles, _duration);
        }
        if (Input.GetKeyDown(KeyCode.A) && _i>0)
        {
            StopTween();
            _i--;
            _cameraTransform.DOMove(_transformPoints[_i].position, _duration);
            _cameraTransform.DORotate(_transformPoints[_i].rotation.eulerAngles, _duration);
        }

        if (Input.GetKeyDown(KeyCode.Space))
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
        StartCoroutine(HideCoroutine());
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
            SceneManager.LoadScene("NEW menu");

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
            t = _transitTime / 2;
            //tColor = _text.color;
            a = 1f;

            while (t >= 0)
            {
                a = Mathf.Lerp(0, 1, t / (_transitTime / 2));
                t -= Time.deltaTime;
                tColor.a = a;
                //_text.color = tColor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
