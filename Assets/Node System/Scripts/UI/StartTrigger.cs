using System;
using System.Collections;
using Node_System.Scripts.Node;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartTrigger : TriggerForNode
    {
        [SerializeField] private AudioClip _calculation;
        [SerializeField] private float _volume;

        [SerializeField, Min(0f)] private float cooldown=5f;

        [SerializeField] private Button _button;


        private Image _image;
        private bool _isActive = true;
        private void OnEnable()
        {
            _button.onClick.AddListener(Trigger);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(Trigger);
        }

        private void Start()
        {
            _image = GetComponent<Image>();
            _image.type = Image.Type.Filled;

        }

        private event Action _triger; 

        public override void SubscribeTrigger(Action action)
        {
            _triger += action;
        }

        public void Trigger()
        { 
            if (_isActive)
            {
                SoundController.sounder.SetSound(_calculation, false, gameObject.name, _volume);
                _triger?.Invoke();
                StartCoroutine(Coroutine());
            }

            IEnumerator Coroutine()
            {
                
                _isActive = false;
                _button.interactable = false;
                _image.fillAmount = 0;
                float lerpTime = Time.time;

                while (Time.time - lerpTime < cooldown)
                {
                    _image.fillAmount = (Time.time - lerpTime) / cooldown;

                    yield return null;
                }

                _isActive = true;
                _button.interactable = true;
            }
            
        }
    }
}
