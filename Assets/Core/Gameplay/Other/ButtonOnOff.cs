using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ButtonOnOff : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects = new List<GameObject>();
    [SerializeField] private bool _startSwitch = true;
    [FormerlySerializedAs("_startActive")] [SerializeField] private bool _isStartActive;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        if(!_startSwitch)
            return;
        _gameObjects.ForEach(x => x.SetActive(_isStartActive));
    }

    public void Switch()
    {
        _gameObjects.ForEach(x => x.SetActive(!x.activeSelf));
    }

    private void OnEnable()
    {
        if (_button == null) return;
        _button.onClick.RemoveListener(Switch);
        _button.onClick.AddListener(Switch);
    }

    private void OnDisable()
    {
        if (_button == null) return;
        _button.onClick.RemoveListener(Switch);
    }
}
