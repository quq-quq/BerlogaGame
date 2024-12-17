using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class LockLevelTextScript : MonoBehaviour
{
    public static LockLevelTextScript SingleTone { get; private set; }

    [SerializeField] private TMP_Text _lockButtonText;
    [SerializeField] private float _delayOfHidingText;
    
    private Coroutine _currentCoroutine;
    private Tween _tweenColor;

    private void Awake()
    {
        if (SingleTone != null && SingleTone != this)
        {
            Destroy(gameObject);
        }
        else
        {
            SingleTone = this;
        }

        _lockButtonText.color = new Color(0, 0, 0, 0);
    }

    public void ShowLockButtonText()
    {
        
        if (_tweenColor != null)
            _tweenColor.Kill();
        if(_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _lockButtonText.color = Color.white;
        _currentCoroutine = StartCoroutine(FullWhiteCoroutine());

        IEnumerator FullWhiteCoroutine()
        {
            yield return new WaitForSeconds(_delayOfHidingText/2);
            _tweenColor = _lockButtonText.DOColor(new Color(0, 0, 0, 0), _delayOfHidingText/2);
            _currentCoroutine = null;
        }
    }
}
