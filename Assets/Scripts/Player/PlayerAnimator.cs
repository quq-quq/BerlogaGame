using System;
using TarodevController;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Animator _anim;

    [SerializeField] private SpriteRenderer _sprite;

    [Header("Settings")]
    [SerializeField, Min(0.01f)]
    private float _idleSpeed = 2;
    [SerializeField, Min(0.01f)]
    private float _runSpeed = 1;

    [SerializeField] private float _maxTilt = 5;
    [SerializeField] private float _tiltSpeed = 20;

    [Header("Particles")][SerializeField] private ParticleSystem _jumpParticles;
    [SerializeField] private ParticleSystem _launchParticles;
    [SerializeField] private ParticleSystem _moveParticles;
    [SerializeField] private ParticleSystem _landParticles;

    [Header("Audio")]
    [SerializeField] private AudioClip _footsteps;
    [SerializeField] private AudioClip _Jump;
    [SerializeField] private float _volume;

    private IPlayerController _player;
    private bool _grounded;
    private ParticleSystem.MinMaxGradient _currentGradient;

    private void Awake()
    {
        _player = GetComponentInParent<IPlayerController>();
        _anim.SetFloat(IdleSpeedKey, _idleSpeed);
        _anim.SetFloat(RunSpeedKey, _runSpeed);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _anim.SetFloat(IdleSpeedKey, _idleSpeed);
        _anim.SetFloat(RunSpeedKey, _runSpeed);
    }
#endif

    private void OnEnable()
    {
        _player.Jumped += OnJumped;
        _player.GroundedChanged += OnGroundedChanged;

        _moveParticles.Play();
    }

    private void OnDisable()
    {
        _player.Jumped -= OnJumped;
        _player.GroundedChanged -= OnGroundedChanged;

        _moveParticles.Stop();
    }

    private void Update()
    {
        if (_player == null) return;

        HandleSpriteFlip();

        HandleCharacterTilt();

        HandleRun();
    }

    private void HandleSpriteFlip()
    {
        if (_player.FrameInput.x != 0) _sprite.flipX = _player.FrameInput.x > 0;
    }

    private void HandleCharacterTilt()
    {
        var runningTilt = _grounded ? Quaternion.Euler(0, 0, _maxTilt * _player.FrameInput.x) : Quaternion.identity;
        _anim.transform.up = Vector3.RotateTowards(_anim.transform.up, runningTilt * Vector2.up, _tiltSpeed * Time.deltaTime, 0f);
    }

    private void HandleRun()
    {
        _anim.SetBool(RunKey, _grounded && _player.FrameInput.x != 0);
        if (_grounded && _player.FrameInput.x != 0)
            SoundController.sounder.SetSound(_footsteps, true, "PlayerRun", _volume);
        if ((_grounded && _player.FrameInput.x == 0) || !_grounded)
            SoundController.sounder.SetSound(null, false,"PlayerRun", _volume);
    }

    private void OnJumped()
    {
        _anim.SetTrigger(JumpKey);
        _anim.ResetTrigger(GroundedKey);


        if (_grounded) // Avoid coyote
        {
            _jumpParticles.Play();
        }
    }

    private void OnGroundedChanged(bool grounded, float impact)
    {
        _grounded = grounded;

        if (grounded)
        {
            _anim.SetTrigger(GroundedKey);
            SoundController.sounder.SetSound(_Jump, false, "PlayerJump", _volume);
            _moveParticles.Play();

            _landParticles.transform.localScale = Vector3.one * Mathf.InverseLerp(0, 40, impact);
            _landParticles.Play();
        }
        else
        {
            _moveParticles.Stop();
        }
    }

    private static readonly int GroundedKey = Animator.StringToHash("Grounded");
    private static readonly int IdleSpeedKey = Animator.StringToHash("IdleSpeed");
    private static readonly int RunSpeedKey = Animator.StringToHash("RunSpeed");
    private static readonly int JumpKey = Animator.StringToHash("Jump");
    private static readonly int RunKey = Animator.StringToHash("IsRun");
}
