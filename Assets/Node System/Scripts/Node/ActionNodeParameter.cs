using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Node_System.Scripts.Node
{
    public abstract class ActionNodeParameter : ActionNode
    {
        [SerializeField] private bool _nonNegative = true;
        [SerializeField,Min(0f)] private float _step = 0.5f;
        [SerializeField] private float _value = 0;
        [SerializeField] private float _maxValue = 10;
        [SerializeField] private float _minValue = -10;
        [SerializeField] private Button _increaseButton;
        [SerializeField] private Button _decreaseButton;        
        [SerializeField] private TMP_Text _textValue;
        
        public float Value
        {
            get => _value;
            private set
            {
                _value = Mathf.Clamp(value, _minValue,_maxValue);
                _textValue.text = _value.ToString();
            }
        }

        private void Awake()
        {
            _textValue.text = _value.ToString();
            _increaseButton.onClick.AddListener(() => Value+=_step);
            _decreaseButton.onClick.AddListener(() => Value = Value - _step < 0 && _nonNegative? 0f: Value - _step);
        }
    }
}
