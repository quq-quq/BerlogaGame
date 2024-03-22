using System.Collections;
using System.Collections.Generic;
using Save_files.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Optioner : MonoBehaviour
{
    [SerializeField] private Image _muteButton;
    [SerializeField] private Sprite _mute;
    [SerializeField] private Sprite _unmute;
    private bool _isMusicOff;

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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
