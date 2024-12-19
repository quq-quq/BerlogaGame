using DG.Tweening;
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
    [SerializeField] private Image _fadeImage;

    private int _index = 0;
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
            _index++;
            _cameraTransform.DOKill();
            _cameraTransform.DOMove(_transformPoints[_index].position, _tweenDuration);
            _cameraTransform.DORotate(_transformPoints[_index].rotation.eulerAngles, _tweenDuration);
        }
        if (Input.GetKeyDown(KeyCode.A) && _index>0)
        {
            _index--;
            _cameraTransform.DOKill();
            _cameraTransform.DOMove(_transformPoints[_index].position, _tweenDuration);
            _cameraTransform.DORotate(_transformPoints[_index].rotation.eulerAngles, _tweenDuration);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hide();          
        }
    }

    //private void StopTween(Tween tween)
    //{
    //    if (tween != null)
    //    {
    //        tween.Kill();
    //        tween = null;
    //    }
    //}

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
        _fadeImage.DOKill();
        _cameraTransform.DOKill();
        _fadeImage.DOColor(Color.black, _hideTransitTime).OnComplete(() => {
            _sceneLoader.LoadScene(_nextSceneData);
        });
    }

    public void Show()
    {
        _fadeImage.DOColor(Color.clear, _hideTransitTime);
    }
}
