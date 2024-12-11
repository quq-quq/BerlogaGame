using System;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Dialogue_system;
using Node_System.Scripts.Node;
using NodeObjects;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CopilotMonolog : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField, Multiline] private List<string> idlePhrase;
    [SerializeField,Min(0f)] private float _timeIdlePhrase;
    [SerializeField,Min(0f)] private float _timeShowPhrase;
    [SerializeField,Min(0f)] private float _timeTextAnimation;
    [SerializeField,Min(0f)] private float _animatorTimeOffset;
    [SerializeField,Min(0f)] private float _charTime;
    [SerializeField] private Animator _speechBalloon;
    private PriorityQueue<string, int> _phrases;
    private static readonly int ShowKey = Animator.StringToHash("Show");
    private static readonly int HideKey = Animator.StringToHash("Hide");
    private WriterDialogue _writer;

    [SerializeField,Min(0f)] private float _interactDis;
    [SerializeField, Range(0f, 1f)] private float _chanceDoor;
    [SerializeField, Multiline] private List<string> _doorOpen;
    [SerializeField, Multiline] private List<string> _doorClose;
    [SerializeField, Range(0f, 1f)] private float _chanceGalagram;
    [SerializeField, Multiline] private List<string> _galagramOn;
    [SerializeField, Multiline] private List<string> _galagramOff;
    [SerializeField, Range(0f, 1f)] private float _chanceCamera;
    [SerializeField, Multiline] private List<string> _cameraSee;
    [SerializeField, Multiline] private List<string> _cameraDontSee;
    [SerializeField, Multiline] private List<string> _starDialog;

    private List<Door> _doors;
    private List<GalagramRobot> _galagrams;
    private List<CameraDetector> _camers;
    private List<StarController> _stars;
    

    private void Start()
    {
        _phrases = new PriorityQueue<string, int>();
        StartCoroutine(IdlePhraseEnqueue());
        StartCoroutine(ShowPhrase());
    }

    private void OnEnable()
    {
        
        _doors = FindObjectsOfType<Door>().ToList();
        _camers = FindObjectsOfType<CameraDetector>().ToList();
        _galagrams = FindObjectsOfType<GalagramRobot>().ToList();

        foreach (var door in _doors)
        {
            door.OpenEvent += OnDoorOpen;
            door.CloseEvent += OnDoorClose;
        }
        foreach (var cam in _camers)
        {
            cam.EnterEvent += OnCamEnter;
            cam.ExitEvent += OnCamExit;
        }
        foreach (var galagram in _galagrams)
        {
            galagram.ActivateEvent += OnGalagramOn;
            galagram.DisactivateEvent += OnGalagramOff;
        }
    }

    private bool _starHint = false;
    private void FixedUpdate()
    {
        _stars = FindObjectsOfType<StarController>().ToList();

        foreach (var star in _stars)
        {
            if ((star.transform.position - transform.position).magnitude <= _interactDis&&!_starHint)
            {
                _starHint = true;
                _phrases.Enqueue(_starDialog[Random.Range(0,_starDialog.Count)], 1);
            }
        }
    }

    private void OnDisable()
    {
        foreach (var door in _doors)
        {
            door.OpenEvent -= OnDoorOpen;
            door.CloseEvent -= OnDoorClose;
        }
        foreach (var cam in _camers)
        {
            cam.EnterEvent -= OnCamEnter;
            cam.ExitEvent -= OnCamExit;
        }
        foreach (var galagram in _galagrams)
        {
            galagram.ActivateEvent -= OnGalagramOn;
            galagram.DisactivateEvent -= OnGalagramOff;
        }
    }

    private void OnDoorOpen()
    {
        EnqueueChance(_chanceDoor,_doorOpen[Random.Range(0,_doorOpen.Count)],2);
    }
    private void OnDoorClose()
    {
        EnqueueChance(_chanceDoor,_doorClose[Random.Range(0,_doorClose.Count)],2);

    }
    private void OnCamEnter()
    {
        EnqueueChance(_chanceCamera,_cameraSee[Random.Range(0,_cameraSee.Count)],2);

    }
    private void OnCamExit()
    {
        EnqueueChance(_chanceCamera,_cameraDontSee[Random.Range(0,_cameraDontSee.Count)],2);

    }
    private void OnGalagramOff()
    {
        EnqueueChance(_chanceGalagram,_galagramOff[Random.Range(0,_galagramOff.Count)],2);

    }
    private void OnGalagramOn()
    {
        EnqueueChance(_chanceGalagram,_galagramOn[Random.Range(0,_galagramOn.Count)],2);

    }

    private void EnqueueChance(float chance, string phrase, int prior)
    {
        var t = Random.Range(0f, 1f);
        if (t<=chance)
        {
            _phrases.Enqueue(phrase, prior);
        }
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
    
    private IEnumerator IdlePhraseEnqueue()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeIdlePhrase);
            _phrases.Enqueue(idlePhrase[Random.Range(0, idlePhrase.Count)], 3);
        }
    }

    private IEnumerator ShowPhrase()
    {
        while (true)
        {
            if (_phrases.Count != 0)
            {
                _text.text = "";
                _speechBalloon.SetTrigger(ShowKey);
                StartCoroutine(TextShow());
                yield return new WaitForSeconds(_animatorTimeOffset);
                yield return WriteTextCoroutine(_phrases.Dequeue());
                yield return new WaitForSeconds(_timeShowPhrase);
                yield return TextHide(); 
                _speechBalloon.SetTrigger(HideKey);
                yield return new WaitForSeconds(_animatorTimeOffset);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator TextShow()
    {
        _text.gameObject.SetActive(true);
        var c = _text.color;
        var t = 0f;
        while (t<=_timeTextAnimation)
        {
            c.a = Mathf.Lerp(0, 1, t / _timeTextAnimation);
            _text.color = c;
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
        }
    }
    
    private IEnumerator TextHide()
    {
        var c = _text.color;
        var t = 0f;
        while (t<=_timeTextAnimation)
        {
            c.a = Mathf.Lerp(1, 0, t / _timeTextAnimation);
            _text.color = c;
            yield return new WaitForFixedUpdate();
            t += Time.fixedDeltaTime;
        }
        _text.gameObject.SetActive(false);
    }

}
