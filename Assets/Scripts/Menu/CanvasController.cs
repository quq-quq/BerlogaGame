using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Canvas _menuLevels;
    [SerializeField] private Canvas _options;
    [SerializeField] private Canvas _resetQuestionMenu;
    [SerializeField] private Canvas _comicsMenu;


    private void Start()
    {
        _menuLevels.gameObject.SetActive(false);
        _options.gameObject.SetActive(false);
        _resetQuestionMenu.gameObject.SetActive(false);
        _comicsMenu.gameObject.SetActive(false);
    }

    public void SwitchLevelMenu()
    {
        _menuLevels.gameObject.SetActive(!_menuLevels.gameObject.activeSelf);
        _options.gameObject.SetActive(false);
        _resetQuestionMenu.gameObject.SetActive(false);
        _comicsMenu.gameObject.SetActive(false);
    }

    public void SwitchOptions()
    {
        _options.gameObject.SetActive(!_options.gameObject.activeSelf);
        _menuLevels.gameObject.SetActive(false);
        _resetQuestionMenu.gameObject.SetActive(false);
        _comicsMenu.gameObject.SetActive(false);
    }

    public void SwitchResetQuestionMenu()
    {
        _resetQuestionMenu.gameObject.SetActive(!_resetQuestionMenu.gameObject.activeSelf);
        _menuLevels.gameObject.SetActive(false);
        _options.gameObject.SetActive(false);
        _comicsMenu.gameObject.SetActive(false);
    }

    public void SwitchComicsMenu()
    {
        _comicsMenu.gameObject.SetActive(!_comicsMenu.gameObject.activeSelf);
        _menuLevels.gameObject.SetActive(false);
        _options.gameObject.SetActive(false);
        _resetQuestionMenu.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
