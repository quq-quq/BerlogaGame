using UnityEngine;

public class ButtonOnOff : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private bool _startActive;

    private void Start()
    {
        _gameObject.SetActive(_startActive);
    }

    public void Switch()
    {
        _gameObject.SetActive(!_gameObject.activeSelf);
    }
}
