using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private GameObject _menuButtons, _drake, _levelsButtons;

    private void Start()
    {
        AudioListener.pause = false;
        _menuButtons.SetActive(true);
        _drake.SetActive(false);
        _levelsButtons.SetActive(false);
    }

    public void LoadScene( int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
