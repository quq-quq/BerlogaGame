using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]  private TextMeshProUGUI _text;
    [SerializeField] private DialogueTrigger _DialogueRobot;

    private void Start()
    {
        _text.text = "Найдите диск...:(";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _text.text = "Диск найден...:D";
            _DialogueRobot.ChangeIndex();
        }
        Destroy(gameObject);
    }
}
