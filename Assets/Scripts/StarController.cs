using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField]  private TextMeshProUGUI _text;

    private void Start()
    {
        _text.text = "������� ���������-����������";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Entety"))
        {
            _text.text = "��������� �������, �����������!";
        }
        Destroy(gameObject);
    }
}
