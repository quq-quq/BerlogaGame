using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _runAudio, _jumpAudio;
    [SerializeField] PlayerController _playerController;
    private AudioSource _audioSource;
    

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = false;
    }

    private void OnEnable()
    {
        _playerController.MoveEvent += MovementSoundsDetecting;
    }
    private void OnDisable()
    {
        _playerController.MoveEvent -= MovementSoundsDetecting;
    }

    private void MovementSoundsDetecting(Vector2 direction)
    {
        if(direction.x!=0 && direction.y == 0)
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.loop = true;
                _audioSource.clip = _runAudio;
                _audioSource.Play();
            }
        }


        if(Input.GetKey(KeyCode.Space))
        {
            _audioSource.loop = false;
            _audioSource.clip = _jumpAudio;
            _audioSource.Play();
        }
        else if(direction.x == 0 && direction.y == 0)
        {
            _audioSource.clip = null;
            _audioSource.Play();
        }

    }
}
