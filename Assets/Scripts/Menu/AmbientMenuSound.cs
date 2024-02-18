using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    [RequireComponent(typeof(AudioSource))]
    public class AmbientMenuSound : MonoBehaviour
    {
        [SerializeField] private float _cycleTime;
        [SerializeField] private List<AmbientSoundData> _sounds;
        [SerializeField] private bool _isEnableAmbientSound = true;
        
        private AudioSource _audioSource;
        
        public bool IsEnableAmbientSound
        {
            get => _isEnableAmbientSound;
            set
            {
                _audioSource.volume = value?1f:0f;
                _isEnableAmbientSound = value;
            }
        }

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            StartCoroutine(AmbientAudioSyncCoroutine());
        }

        IEnumerator AmbientAudioSyncCoroutine()
        {
            var i = 0;
            while (true)
            {
                float waitTime;
                if(i < _sounds.Count)
                {
                    waitTime = _sounds[i].Time - (i == 0 ? 0 : _sounds[i - 1].Time);
                }
                else
                {
                    waitTime = _cycleTime - _sounds[^1].Time + _sounds[0].Time;
                    i = 0;
                }
                
                yield return new WaitForSeconds(waitTime);

                _audioSource.Stop();
                _audioSource.clip = _sounds[i].Sound;
                _audioSource.Play();
                i++;
            }
        }

        [Serializable]
        public class AmbientSoundData
        {
            [SerializeField] private AudioClip _sound;
            [SerializeField] private float _time;

            public AudioClip Sound
            {
                get => _sound;
            }

            public float Time
            {
                get => _time;
            }
        }
    }
}
