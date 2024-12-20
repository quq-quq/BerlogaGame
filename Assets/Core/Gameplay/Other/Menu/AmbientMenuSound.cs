using Save_files.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class AmbientMenuSound : MonoBehaviour
    {
        [SerializeField] private float _cycleTime;
        [SerializeField] private List<AmbientSoundData> _sounds;
        [SerializeField] private bool _isEnableAmbientSound = true;
        [SerializeField, Range(0, 1)] private float _volume;
        
        public bool IsEnableAmbientSound
        {
            get => _isEnableAmbientSound;
            set
            {
                _isEnableAmbientSound = value;
            }
        }

        private void Awake()
        {
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

                SoundController.sounder.SetSound(_sounds[i].Sound, false, gameObject.name, _volume);
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
