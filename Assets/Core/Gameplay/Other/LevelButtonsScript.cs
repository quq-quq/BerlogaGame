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
    [SerializeField] private Transform _parentOfButtons;
    [SerializeField] private Image _manualButtonImage;
    [SerializeField] private Sprite _manualActiveSprite;
    [SerializeField] private Sprite _manualInactiveSprite;
    [SerializeField] private Image _muteButtonImage;
    [SerializeField] private Sprite _muteSprite;
    [SerializeField] private Sprite _unmuteSprite;
    [SerializeField] private Image _pauseButtonImage;
    [SerializeField] private Sprite _pausedSprite;
    [SerializeField] private Sprite _unpausedSprite;
    [SerializeField] private GameObject _manual;
    [SerializeField] private SceneData _menuScene;
    [SerializeField] private Button _suggestionButton;
    [SerializeField, Min(0f)] private float _suggestionCooldown = 5f;
    [SerializeField] private CopilotMonolog _copilotMonolog;
    [SerializeField] private GameObject _nodeDesk, _buttonDesk;
    [SerializeField] private StateOfPlayerController _stateOfPlayerController;
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
            _muteButtonImage.sprite = _muteSprite;
        }
        else
        {
            _muteButtonImage.sprite = _unmuteSprite;
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
            _pauseButtonImage.sprite = _pausedSprite;
            _isPaused = true;
            _pausePanel.SetActive(true);
            SetOnlyCurrentButton(_pauseButtonImage.gameObject);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1f;
            _pausePanel.SetActive(false);
            _isPaused = false;
            _pauseButtonImage.sprite = _unpausedSprite;
            SetAllButtonsOn();
        }
        _stateOfPlayerController.SetStateOfComponent();
    }

    public void StopMusic()
    {
        if (!_isMusicOff)
        {
            AudioListener.pause = true;
            _isMusicOff = true;
            _muteButtonImage.sprite = _muteSprite;
            Saver.Data.IsMute = true;
        }
        else
        {
            AudioListener.pause = false;
            _isMusicOff = false;
            _muteButtonImage.sprite = _unmuteSprite;
            Saver.Data.IsMute = false;
        }
        Saver.Save();
    }

    public void OpenCloseManual()
    {
       _manual.SetActive(!_manual.activeSelf);
        if (_manual.activeSelf)
        {
            SetOnlyCurrentButton(_manualButtonImage.gameObject);
            _manualButtonImage.sprite = _manualActiveSprite;
        }
        else
        {
            SetAllButtonsOn();
            _manualButtonImage.sprite = _manualInactiveSprite;
        }
        _stateOfPlayerController.SetStateOfComponent();
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

    public void SetOnlyCurrentButton(GameObject currentButton)
    {
        bool on = false;

        foreach (Transform button in _parentOfButtons)
        {
            if(button.gameObject == currentButton)
            {
                continue;
            }
            if (button.gameObject.activeSelf)
            {
                on = true;
                break;
            }
        }

        if (on)
        {
            foreach (Transform button in _parentOfButtons)
            {
                if (button.gameObject != currentButton)
                {
                    button.gameObject.SetActive(false);
                }
            }
            if(currentButton != _buttonDesk)
                _buttonDesk.SetActive(false);
        }
        else
        {
            SetAllButtonsOn();
        }
    }

    public void SetAllButtonsOn()
    {
        foreach (Transform button in _parentOfButtons)
        {
            button.gameObject.SetActive(true);   
        }
        _buttonDesk.SetActive(true);
    }

    public void SetAllButtonsOff()
    {
        foreach (Transform button in _parentOfButtons)
        {
            button.gameObject.SetActive(false);
        }
        _buttonDesk.SetActive(false);
    }
}
