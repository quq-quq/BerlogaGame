using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private List<Canvas> canvasList;
    [Space(20)]
    [SerializeField] private float _showTransitTime;
    [SerializeField] Image _image;


    private void Awake()
    {
        _image.color = Color.black;
    }

    private void Start()
    {
        foreach (Canvas i in canvasList)
        {

           i.gameObject.SetActive(false);
        }
        Show();
    }

    public void SwitchMenu(Canvas currentCanvas)
    {
        currentCanvas.gameObject.SetActive(!currentCanvas.gameObject.activeSelf);

        foreach (Canvas i in canvasList)
        {
            if(i != currentCanvas)
                i.gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Show()
    {
        StartCoroutine(ShowCoroutine());
        IEnumerator ShowCoroutine()
        {
            var t = _showTransitTime;
            var tColor = Color.black;
            var a = 1f;

            while (t > 0)
            {
                a = Mathf.Lerp(0, 1, t / _showTransitTime);
                t -= Time.deltaTime;
                tColor.a = a;
                _image.color = tColor;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
