using DG.Tweening;
using System.Collections;
using Core.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using System.Collections.Generic;

public class ComicsMover : MonoBehaviour
{
    [SerializeField]private List<Transform> _transformPoints;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField]private Transform _parentOfPoints;
    [Space(20)]
    [SerializeField] private SceneData _nextSceneData;
    [Space(20)]
    [SerializeField] private float _tweenDuration;
    [SerializeField] private float _showTransitTime;
    [SerializeField] private float _hideTransitTime;
    [Space(20)]
    [SerializeField] private Image _image;

    private int _index = 0;
    private bool _isHiding = false;
    private Tween _currentTween;
    private SceneLoader _sceneLoader;

    [Inject]
    private void Inject(SceneLoader loader)
    {
        _sceneLoader = loader;
    }
    
    private void Start()
    {

        _cameraTransform.position = _transformPoints[0].position;

        Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && _index < _transformPoints.Count-1)
        {
            StopTween();
            _index++;
            _cameraTransform.DOMove(_transformPoints[_index].position, _tweenDuration);
            _cameraTransform.DORotate(_transformPoints[_index].rotation.eulerAngles, _tweenDuration);
        }
        if (Input.GetKeyDown(KeyCode.A) && _index>0)
        {
            StopTween();
            _index--;
            _cameraTransform.DOMove(_transformPoints[_index].position, _tweenDuration);
            _cameraTransform.DORotate(_transformPoints[_index].rotation.eulerAngles, _tweenDuration);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_isHiding)
        {
            Hide();          
        }

    }

    private void StopTween()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
            _currentTween = null;
        }
    }

    public void CreateTransformPointForEditor()
    {
        _transformPoints.RemoveAll(t => t == null);
        for(int i = 0; i < _transformPoints.Count; i++)
        {
            _transformPoints[i].gameObject.name = $"TransformPoint{i}";
        }

        GameObject newPoint = new GameObject($"TransformPoint{_transformPoints.Count}");
        
        newPoint.GetComponentInParent<Transform>().position = _cameraTransform.position;
        newPoint.GetComponentInParent<Transform>().rotation = _cameraTransform.rotation;
        newPoint.transform.parent = _parentOfPoints;

        _transformPoints.Add(newPoint.transform);
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

            _sceneLoader.LoadScene(_nextSceneData);
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
