using Core.Gameplay.SceneManagement;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class SceneSwitchByTimeOrSkipWithFade : MonoBehaviour
{
     [SerializeField] private SceneData _nextScene;
     [SerializeField] private Image _fade;
     [SerializeField] private float _time;
     
     private SceneLoader _sceneLoader;
     private Sequence _sequence;

     [Inject]
     private void Inject(SceneLoader sceneLoader)
     {
          _sceneLoader = sceneLoader;
     }

     private void Start()
     {
          var s = DOTween.Sequence().AppendInterval(_time).Append(_fade.DOFade(1, 1f)).OnComplete(LoadNext);
          s.Play();
          _sequence = s;
     }

     private void Update()
     {
          if(Input.anyKeyDown)
          {
               DOTween.Kill(_sequence);
               _fade.DOFade(1, 1f).OnComplete(LoadNext);
          }
     }

     private void LoadNext()
     {
          _sceneLoader.LoadScene(_nextScene);
     }
     
}