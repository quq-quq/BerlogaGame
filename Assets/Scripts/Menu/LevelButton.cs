using System;
using System.Collections.Generic;
using Save_files.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Menu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _background;
        [SerializeField] private Image _lock;
        [SerializeField] private Sprite _close;
        [SerializeField] private Sprite _incomplite;
        [SerializeField] private Sprite _complited;
        [Space] [SerializeField] private bool _isHanded = false;
        [SerializeField] private string _handedSceneName;
        

        private bool _isAvailable;
        private LevelPart _levelPart;
        public void Init(LevelPart levelPart, int ordinal, bool enable)
        {
            _lock.gameObject.SetActive(!enable);
            _levelPart = levelPart;
            _text.text = ordinal.ToString();
            _isAvailable = enable;
            if (enable)
            {
                if (_levelPart.IsCompleted)
                {
                    _background.sprite = _complited;
                }
                else
                {
                    _background.sprite = _incomplite;
                }
            }
            else
            {
                _background.sprite = _close;
            }
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (_isHanded)
            {
                SceneManager.LoadScene(_handedSceneName);
                return;
            }
            
            if (_isAvailable)
            {
                SceneManager.LoadScene(_levelPart.SceneName);
            }
            else
            {
                //show message that say u cant play this level
            }
        }
        
    }
}