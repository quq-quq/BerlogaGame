﻿using System;
using System.Collections;
using System.Collections.Generic;
using Node_System.Scripts.Node;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartTrigger : TriggerForNode
    {
        [SerializeField] private List<AudioClip> _calculations;
        [SerializeField] private float _volume;

        [SerializeField, Min(0f)] private float cooldown=5f;

        [SerializeField] private Button _button;


        private MonoBehaviour _alwaysOnGO;
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

        private void Awake()
        {
            _image = GetComponent<Image>();
            _image.type = Image.Type.Filled;
            _alwaysOnGO = Camera.main.GetComponent<MonoBehaviour>();
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
                SoundController.sounder.SetSound(GetRandomAudioClip(_calculations), false, gameObject.name, _volume);
                _triger?.Invoke();
                _alwaysOnGO.StartCoroutine(Coroutine());
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

        public override void SetTitle(string title)
        {
        }

        private AudioClip GetRandomAudioClip(List<AudioClip> list)
        {
            if (list == null || list.Count == 0)
            {
                return null;
            }

            System.Random random = new System.Random();
            int randomIndex = random.Next(list.Count);
            return list[randomIndex];
        }
    }
}
