using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] private DialogueTrigger _DialogueRobot;
    [SerializeField] private AudioClip _audioClip;
    [Space]
    [SerializeField, Range(0, 1)] private float _volume = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _DialogueRobot.ChangeIndex();
        }

        SoundController.sounder.SetSound(_audioClip, false, gameObject.name, _volume);
        Destroy(gameObject);
    }
}
