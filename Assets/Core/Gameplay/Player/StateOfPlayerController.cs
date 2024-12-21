using Core.Gameplay.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _mainUI;
    [SerializeField] List<Transform> _notDisablePlayerControllerPanels;
    private List<Transform> _panels = new List<Transform>();
    private bool _isEnabled = true;

    private void Start()
    {
        foreach ( Transform panel in _mainUI)
        {
            if(!_notDisablePlayerControllerPanels.Contains(panel) && panel.gameObject.name != "EventSystem")
                _panels.Add(panel);
        }
    }

    private void Update()
    {
        foreach (RectTransform panel in _panels)
        {
            if (panel.GetChild(0).gameObject.activeSelf)
            {
                _playerController.enabled = false;
                _playerController.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                SoundController.sounder.SetSound(null, false, "PlayerRun", 0);
                return;
            }
        }
        _playerController.enabled = true;
    }
}
