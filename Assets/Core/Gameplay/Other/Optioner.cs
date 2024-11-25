using System.Collections;
using System.Collections.Generic;
using Core.Gameplay.SceneManagement;
using Save_files.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class Optioner : MonoBehaviour
{
    [SerializeField] private Image _muteButton;
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    [SerializeField] private Slider _volumeSlider;
    private bool _isMusicOff;

    private SceneLoader _sceneLoader;

    [Inject]
    private void Inject(SceneLoader loader)
    {
        _sceneLoader = loader;
    }


    private void Start()
    {
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
        _volumeSlider.value = Saver.Data.Volume;
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

    public void DeleteSave()
    {
        Saver.DeleteSaves();
        _sceneLoader.LoadScene(_sceneLoader.GetCurrentScene());
    }

    public void OnSliderValueChange(float f)
    {
        Saver.Data.Volume = f;
        SoundController.sounder.VolumeChange();
    }

}
