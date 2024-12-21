using System.Collections;
using Core.Gameplay.SceneManagement;
using DefaultNamespace;
using DG.Tweening;
using Save_files.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class StartEndScene : MonoBehaviour
{
    [SerializeField] private SceneData _nextScene;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private TriggerZone _zone;
    [SerializeField] private AudioClip _endAudio;
    [SerializeField] private float _volume;
    [SerializeField] private float _ofsetBeforeAudioClip;
    
    private SceneLoader _sceneLoader;
    private float _transitTime;

    public TMP_Text Text
    {
        get => _text;
    }

    [Inject]
    private void Inject(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    private void Awake()
    {
        _transitTime = _endAudio.length + _ofsetBeforeAudioClip;
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
        
        
        IEnumerator HideCoroutine()
        {
            _image.DOColor(Color.black, _transitTime);
            yield return new WaitForSeconds(_ofsetBeforeAudioClip);
            SoundController.sounder.SetSound(_endAudio, false, "BackGroundMusic", _volume);
            yield return new WaitForSeconds(_transitTime-_ofsetBeforeAudioClip);

            Saver.Data.SetCompleted(_sceneLoader.GetCurrentScene());
            _sceneLoader.LoadScene(_nextScene);
        }
    }

    public void Show()
    {
        StartCoroutine(ShowCoroutine());


        IEnumerator ShowCoroutine()
        {
           yield return _image.DOColor(Color.clear, _transitTime).WaitForCompletion();
           _text.DOColor(Color.clear, _transitTime);
        }

        
        
    }
}
