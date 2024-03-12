using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonsScript : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private AudioClip _backgroundSound;
    [SerializeField] private float _volume;
    private bool _isPaused, _isMusicOff;

    private void Start()
    {
        AudioListener.pause = false;
        _isMusicOff = false;
        _pausePanel.SetActive(false);
        _isPaused = false;

        SoundController.sounder.SetSound(_backgroundSound, true, gameObject.name, _volume);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(2);
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
        }
        else
        {
            AudioListener.pause = false;
            _isMusicOff = false;
        }
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        _pausePanel.SetActive(false);
        _isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
