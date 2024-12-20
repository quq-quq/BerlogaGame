using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Dialogue_system;
using DG.Tweening;

[RequireComponent(typeof(CopilotMove))]
[RequireComponent(typeof(Transform))]
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
    [SerializeField] private TMP_Text _textPhrase;
    [SerializeField] private TMP_Text _textQuestion;
    [SerializeField] private CanvasGroup _phraseCanvasGroup;
    [SerializeField] private CanvasGroup _questionCanvasGroup;
    [SerializeField] private Animator _speechBableAnimator;
    [SerializeField] private AnimationClip _hideKeyAnimationClip;
    [SerializeField] private Button _yesForPhraseButton;
    [SerializeField] private Button _noForPhraseButton;
    [Space(10)]
    [SerializeField] private float _timeOfChar;
    [SerializeField, Min(0f)] private float _timeAnimatorOffset;
    [SerializeField, Min(0)] private float _timeShow;
    [SerializeField, Min(0f)] private float _timeCanvasGroupFade;
    [Space(30)]
    [SerializeField] private List<Zone> _zones;
    [Space(30)]
    [Header("Appearence Parametrs")]
    [SerializeField] private Transform _copilotPointsTransform;
    [Space(10)]
    [SerializeField, Min(0f)] private float _timeToNextAppearence;

    private int _distance = 20;
    private int _currentZoneIndex;
    private WriterDialogue _writer;
    private Coroutine _currentCoroutine;
    private readonly int ShowKey = Animator.StringToHash("Show");
    private readonly int HideKey = Animator.StringToHash("Hide");

    private void Awake()
    {
        _currentCoroutine = null;
        _currentZoneIndex = 0;

        //For savety (just in case)
        //_phraseCanvasGroup.alpha = 0;
        //_questionCanvasGroup.alpha = 0;
        //_yesForPhraseButton.interactable = false;
        //_noForPhraseButton.interactable = false;
        //_phraseCanvasGroup.gameObject.SetActive(false);
        //_questionCanvasGroup.gameObject.SetActive(false);

        HideCopilot();
    }

    private void OnEnable()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = null;
   
        _yesForPhraseButton?.onClick.AddListener(TakeSuggestion);
        _noForPhraseButton?.onClick.AddListener(() => StartCoroutine(HideQuestion()));
    }

    private void OnDisable()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        _currentCoroutine = null;

        _yesForPhraseButton?.onClick.RemoveListener(TakeSuggestion);
        _noForPhraseButton?.onClick.RemoveListener(() => StartCoroutine(HideQuestion()));
    }

    private void TakeSuggestion()
    {
        if (_currentCoroutine == null)
        {
            _currentCoroutine = StartCoroutine(ShowPhrase());
        }
    }

    private IEnumerator ShowPhrase()
    {
        _questionCanvasGroup.DOFade(0, _timeCanvasGroupFade).WaitForCompletion();
        _speechBableAnimator.SetTrigger(HideKey);
        yield return new WaitForSeconds(_hideKeyAnimationClip.length);
        yield return new WaitForSeconds(_timeAnimatorOffset);

        _questionCanvasGroup.gameObject.SetActive(false);
        _phraseCanvasGroup.gameObject.SetActive(true);
        _phraseCanvasGroup.alpha = 0;

        _currentZoneIndex = CheckZoneIndex();
        Zone currentZone = _zones[_currentZoneIndex];

        if (_zones[_currentZoneIndex].phrasesConfig.Phrases.Count > 0)
        {
            _textPhrase.text = "";
            _speechBableAnimator.SetTrigger(ShowKey);
            yield return new WaitForSeconds(_timeAnimatorOffset);
            _phraseCanvasGroup.DOFade(1, _timeCanvasGroupFade);
            yield return WriteTextCoroutine(_textPhrase, currentZone.phrasesConfig.Phrases[currentZone.indexOfPhrase]);
            yield return new WaitForSeconds(_timeShow);
            _phraseCanvasGroup.DOFade(0, _timeCanvasGroupFade).WaitForCompletion();
            _speechBableAnimator.SetTrigger(HideKey);
            yield return new WaitForSeconds(_hideKeyAnimationClip.length);
            yield return new WaitForSeconds(_timeAnimatorOffset);

            if (_zones[_currentZoneIndex].indexOfPhrase < _zones[_currentZoneIndex].phrasesConfig.Phrases.Count - 1)
            {
                currentZone.indexOfPhrase++;
                _zones[_currentZoneIndex] = currentZone;
            }
            else
            {
                currentZone.indexOfPhrase = 0;
                _zones[_currentZoneIndex] = currentZone;
            }
        }

        HideCopilot();

        _currentCoroutine = null;

        yield return null;
    }

    private IEnumerator ShowQuestion()
    {
        _phraseCanvasGroup.gameObject.SetActive(false);
        _questionCanvasGroup.gameObject.SetActive(true);
        _questionCanvasGroup.alpha = 0;

        _textQuestion.text = "";
        _speechBableAnimator.SetTrigger(ShowKey);
        yield return new WaitForSeconds(_timeAnimatorOffset);
        _questionCanvasGroup.DOFade(1, _timeCanvasGroupFade);
        yield return WriteTextCoroutine(_textQuestion, "Хочешь подсказку?");

        _currentCoroutine = null;

        yield return null;
    }

    private IEnumerator HideQuestion()
    {
        _questionCanvasGroup.DOFade(0, _timeCanvasGroupFade).WaitForCompletion();
        _speechBableAnimator.SetTrigger(HideKey);
        yield return new WaitForSeconds(_hideKeyAnimationClip.length);
        yield return new WaitForSeconds(_timeAnimatorOffset);
        HideCopilot();

        yield return null;
    }

    private IEnumerator ShowCopilot()
    {
        yield return new WaitForSeconds(_timeToNextAppearence);
        _copilotPointsTransform.Translate(Vector3.down * _distance);
        StartCoroutine(ShowQuestion());
    }

    private void HideCopilot()
    {
        _copilotPointsTransform.Translate(Vector3.up * _distance);
        StartCoroutine(ShowCopilot());
    }

    private IEnumerator WriteTextCoroutine(TMP_Text text, string str)
    {
        _writer = new DecodingWriterDialogue(str);
        while (text.text != _writer.EndText())
        {
            text.text = _writer.WriteNextStep();
            yield return new WaitForSeconds(_timeOfChar);
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
