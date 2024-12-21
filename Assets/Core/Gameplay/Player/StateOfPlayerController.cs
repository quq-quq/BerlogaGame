using Core.Gameplay.UISystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateOfPlayerController : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Transform _mainUI;
    [SerializeField] List<UIPanel> _notDisablePlayerControllerPanels;
    private List<UIPanel> _allPanels = new List<UIPanel>();
    private bool _isEnabled = true;

    private void Awake()
    {
        UIPanel[] panels = _mainUI.GetComponentsInChildren<UIPanel>(true);

        _allPanels.AddRange(panels);
    }

    private void Update()
    {
        foreach(UIPanel panel in _allPanels)
        {
            if (!panel.IsHided && !_notDisablePlayerControllerPanels.Contains(panel))
            {
                _playerController.enabled = false;
                return;
            }
        }
        _playerController.enabled = true;
    }
}
