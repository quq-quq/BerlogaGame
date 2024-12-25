using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarController : MonoBehaviour
{

    [SerializeField] private DialogueTrigger _DialogueRobot;
    [SerializeField] private AudioClip _audioClip;
    [Space]
    [SerializeField, Min(0)] private float _distanceUp;
    [SerializeField] private float _rotation;
    [SerializeField, Min(0)] private float _durationOfMove;
    [SerializeField, Range(0, 1)] private float _volume = 0.5f;

    private void Start()
    {
        transform.DOMoveY(transform.position.y+_distanceUp, _durationOfMove).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 0, _rotation) , _durationOfMove).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _DialogueRobot.ChangeIndex();
        }

        SoundController.sounder.SetSound(_audioClip, false, gameObject.name, _volume);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
