using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _runAudio, _jumpAudio;
    [SerializeField] private float _volume;
    private PlayerControllerOLD _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerControllerOLD>();
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
        if(direction.x!=0 && !_playerController.isJump)
        {
            SoundController.sounder.SetSound(_runAudio, true, gameObject.name, _volume);
        }

        if (Input.GetKey(KeyCode.Space) && !_playerController.isJump)
        {
            SoundController.sounder.SetSound(_jumpAudio, false, gameObject.name, _volume);
        }

        if (direction.x == 0 && direction.y == 0)
        {
            SoundController.sounder.SetSound(null, false, gameObject.name, _volume);
        }
    }
}
