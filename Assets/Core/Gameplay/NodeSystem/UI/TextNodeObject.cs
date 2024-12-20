using TMPro;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _canvasGroup;
    
    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }
    
    public void Show()
    {
        _canvasGroup.alpha = 1;
    }
    
    public void SetText(string text)
    {
        _text.text = text;
    }
}
