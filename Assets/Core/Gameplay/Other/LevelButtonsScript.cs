using Core.Gameplay.SceneManagement;
using Save_files.Scripts;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class LevelButtonsScript : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioClip _backgroundSound;
    [SerializeField] private float _volume;
    [SerializeField] private Image _muteButton;
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private GameObject _manual;
    [SerializeField] private SceneData _menuScene;
    [SerializeField] private Button _suggestionButton;
    [SerializeField, Min(0f)] private float _suggestionCooldown = 5f;
    [SerializeField] private CopilotMonolog _copilotMonolog;
    private bool _isPaused, _isMusicOff, _suggestionIsActive = true, _isCoolDownAccepted = false;
    private SceneLoader _sceneLoader;
    private Image _suggestionImage;

    [Inject]
    private void Inject(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    private void Start()
    {
        _manual.SetActive(false);
        AudioListener.pause = Saver.Data.IsMute;
        if(Saver.Data.IsMute)
        {
            _muteButton.sprite = _mute;
        }
        else
        {
            _muteButton.sprite = _unmute;
        }
        _isMusicOff = Saver.Data.IsMute;
        _pausePanel.SetActive(false);
        _isPaused = false;

        SoundController.sounder.SetSound(_backgroundSound, true, "BackGroundMusic", _volume);

        _suggestionImage = _suggestionButton.gameObject.GetComponent<Image>();
        _suggestionImage.type = Image.Type.Filled;
    }

    public void QuitLevel()
    {
        Time.timeScale = 1f;
        _sceneLoader.LoadScene(_menuScene);
    }

    public void Pause()
    {
        if (!_isPaused)
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
            _isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
            _isPaused = false;
        }
    }

    public void StopMusic()
    {
        if (!_isMusicOff)
        {
            AudioListener.pause = true;
            _isMusicOff = true;
            _muteButton.sprite = _mute;
            Saver.Data.IsMute = true;
        }
        else
        {
            AudioListener.pause = false;
            _isMusicOff = false;
            _muteButton.sprite = _unmute;
            Saver.Data.IsMute = false;
        }
        Saver.Save();
    }

    public void OpenCloseManual()
    {
       _manual.SetActive(!_manual.activeSelf);
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _isPaused = false;
        _sceneLoader.LoadScene(_sceneLoader.GetCurrentScene());
    }
    public void Suggestion()
    {
         _copilotMonolog.TakeSuggestion();

        if (_suggestionIsActive)
        {
            StartCoroutine(Coroutine());
        }

        IEnumerator Coroutine()
        {

            _suggestionIsActive = false;
            _suggestionButton.interactable = false;
            _suggestionImage.fillAmount = 0;
            float lerpTime = Time.time;

            while (Time.time - lerpTime < _suggestionCooldown)
            {
                _suggestionImage.fillAmount = (Time.time - lerpTime) / _suggestionCooldown;

                yield return null;
            }

            _suggestionIsActive = true;
            _suggestionButton.interactable = true;
        }
    }
}
