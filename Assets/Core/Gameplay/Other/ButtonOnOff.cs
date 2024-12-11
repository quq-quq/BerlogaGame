using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ButtonOnOff : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjects;
    [SerializeField] private bool _startActive;

    private void Start()
    {
        _gameObjects.ForEach(x => x.SetActive(_startActive));
    }

    public void Switch()
    {
        _gameObjects.ForEach(x => x.SetActive(!x.activeSelf));
            ;
    }
}
