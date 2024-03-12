using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Canvas _menuLevels;
    [SerializeField] private Canvas _options;

    private void Start()
    {
        _menuLevels.gameObject.SetActive(false);
        _options.gameObject.SetActive(false);
    }

    public void SwitchLevelMenu()
    {
        _menuLevels.gameObject.SetActive(!_menuLevels.gameObject.activeSelf);
        _options.gameObject.SetActive(false);
    }
    
    public void SwitchOptions()
    {
        _options.gameObject.SetActive(!_options.gameObject.activeSelf);
        _menuLevels.gameObject.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
