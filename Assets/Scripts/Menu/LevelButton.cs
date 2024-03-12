using System;
using System.Collections.Generic;
using Save_files.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Menu
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private List<Image> _backgrounds;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _text;

        private Level _level;
        public void Init(Level level, int ordinal)
        {
            _level = level;
            _backgrounds.ForEach(x => x.gameObject.SetActive(false));
            var index = Random.Range(0, _backgrounds.Count);
            _backgrounds[index].gameObject.SetActive(true);
            _button.targetGraphic = _backgrounds[index];
            _text.text = ordinal.ToString();
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
           print(_level.Parts[0].SceneName); 
        }
        
    }
}