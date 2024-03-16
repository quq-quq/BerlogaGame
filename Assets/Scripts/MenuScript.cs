using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtons, _drake, _levelsButtons;
    [SerializeField] private AudioClip _backgroundSound, _logoSound;
    [SerializeField] private float _volume;

    private void Start()
    {
        AudioListener.pause = false;
        _menuButtons.SetActive(true);
        _drake.SetActive(false);
        _levelsButtons.SetActive(false);

        SoundController.sounder.SetSound(_backgroundSound, true, gameObject.name, _volume);

    }

    public void LoadScene( int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GetLogoSound()
    {
        SoundController.sounder.SetSound(_logoSound, false, gameObject.name, _volume);
    }
}
