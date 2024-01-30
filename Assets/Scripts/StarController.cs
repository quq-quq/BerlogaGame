using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]  private TextMeshProUGUI _text;

    private void Start()
    {
        _text.text = "Найдите сообщение-голограмму";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Entety"))
        {
            _text.text = "Голограма найдена, поздравляем!";
        }
        Destroy(gameObject);
    }
}
