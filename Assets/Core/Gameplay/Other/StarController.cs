using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] private DialogueTrigger _DialogueRobot;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _DialogueRobot.ChangeIndex();
        }
        Destroy(gameObject);
    }
}
