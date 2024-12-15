using Dialogue_system;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CopilotMonolog : MonoBehaviour
{
    [System.Serializable]
    private struct Zone
    {
        public Collider2D collider2D;
        public CopilotPhrasesConfig phrasesConfig;
        [HideInInspector]public int indexOfPhrase;
    }

    [Header("Suggestion Parametrs")]
    [SerializeField] private Collider2D _player;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Animator _speechBableAnimator;
    [SerializeField] private AnimationClip _hideKeyAnimationClip;
    [Space(10)]
    [SerializeField] private float _charTime;
    [SerializeField, Min(0f)] private float _timeAnimatorOffset;
    [SerializeField, Min(0)] private float _timeShowPhrase;
    [SerializeField, Min(0f)] private float _timeTextAnimation;
    [Space(30)]
    [SerializeField] private List<Zone> _zones;
    [Space(30)]
    [Header("Appearence Parametrs")]
    [SerializeField] private Transform _copilotPointsTransform;
    [SerializeField] private CopilotMove _copilotMove;
    [Space(10)]
    [SerializeField, Min(0)] private float _distance;

    private int _currentZoneIndex;
    private WriterDialogue _writer;
    private Coroutine _currentCoroutine;
    private readonly int ShowKey = Animator.StringToHash("Show");
    private readonly int HideKey = Animator.StringToHash("Hide");

    public void TakeSuggestion()
    {
        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(ShowPhrase());
        }
    }

    private void Awake()
    {
        _currentCoroutine = null;
        _copilotPointsTransform.Translate(Vector3.up * _distance);
    }

    private IEnumerator ShowPhrase()
    {
        _copilotPointsTransform.Translate(Vector3.down * _distance);
        float _timeAppearence = _distance/_copilotMove.MaxSpeed/2;
        yield return new WaitForSeconds(_timeAppearence);

        _currentZoneIndex = CheckZoneIndex();

        if (_zones[_currentZoneIndex].phrasesConfig.Phrases.Count > 0)
        {
            _text.text = "";
            _speechBableAnimator.SetTrigger(ShowKey);
            StartCoroutine(TextShow());
            yield return new WaitForSeconds(_timeAnimatorOffset);
            yield return WriteTextCoroutine(_zones[_currentZoneIndex].phrasesConfig.Phrases[_zones[_currentZoneIndex].indexOfPhrase]);
            yield return new WaitForSeconds(_timeShowPhrase);
            yield return TextHide();
            _speechBableAnimator.SetTrigger(HideKey);
            yield return new WaitForSeconds(_hideKeyAnimationClip.length);
            yield return new WaitForSeconds(_timeAnimatorOffset);

            if (_zones[_currentZoneIndex].indexOfPhrase < _zones[_currentZoneIndex].phrasesConfig.Phrases.Count - 1)
            {
                Zone zone = _zones[_currentZoneIndex];
                zone.indexOfPhrase++;
                _zones[_currentZoneIndex] = zone;
            }
            else
            {
                Zone zone = _zones[_currentZoneIndex];
                zone.indexOfPhrase = 0;
                _zones[_currentZoneIndex] = zone;
            }
        }

        _currentCoroutine = null;

        _copilotPointsTransform.Translate(Vector3.up * _distance);

        yield return null;
    }


    private IEnumerator WriteTextCoroutine(string text)
    {
        _writer = new DecodingWriterDialogue(text);
        while (_text.text != _writer.EndText())
        {
            _text.text = _writer.WriteNextStep();
            yield return new WaitForSeconds(_charTime);
        }
    }

    private IEnumerator TextHide()
    {
        var c = _text.color;
        var t = 0f;
        while (t <= _timeTextAnimation)
        {
            c.a = Mathf.Lerp(1, 0, t / _timeTextAnimation);
            _text.color = c;
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
        }
        _text.gameObject.SetActive(false);
    }

    private IEnumerator TextShow()
    {
        _text.gameObject.SetActive(true);
        var c = _text.color;
        var t = 0f;
        while (t <= _timeTextAnimation)
        {
            c.a = Mathf.Lerp(0, 1, t / _timeTextAnimation);
            _text.color = c;
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
        }
    }

    private int CheckZoneIndex()
    {
        foreach (var zone in _zones)
        {
            if (zone.collider2D.IsTouching(_player))
            {
                return _zones.IndexOf(zone);
            }
        }
        return _currentZoneIndex;
    }
}
