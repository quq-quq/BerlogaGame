using Core.Extension;
using Core.Gameplay.SceneManagement;
using Core.Gameplay.UISystem;
using Save_files.Scripts;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class SoundController : MonoBehaviour
{
    public static SoundController sounder { get; private set; }

    [SerializeField] private int _sourcesCount;
    [Space]
    [SerializeField] private AudioClip _backGroundMusic;
    [SerializeField, Range(0, 1)] private float _volumeOfBackgroundMusic;


    private SceneLoader _sceneLoader;

    [Inject]
    private void Inject(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    private float VolumeSaver
    {
        get => Saver.Data.Volume;
    }

    private struct AudioData
    {
        public AudioSource audioSource;
        public float personalVolume;
        public string objectName;
    }

    private AudioData[] _audioDataArray;

    private void Awake()
    {
        sounder = this;
        if (gameObject.GetComponents<AudioSource>().Length != _sourcesCount)
        {
            for (int i = 0; i < _sourcesCount; i++)
            {
                gameObject.AddComponent<AudioSource>();
            }
        }

        _audioDataArray = new AudioData[_sourcesCount];
        for (int i = 0; i < _sourcesCount; i++)
        {
            _audioDataArray[i].audioSource = gameObject.GetComponents<AudioSource>()[i];
            _audioDataArray[i].audioSource.clip = null;
            _audioDataArray[i].audioSource.loop = false;
            _audioDataArray[i].audioSource.volume = 0;
        }
    }

    private void Start()
    {
        SetSound(_backGroundMusic, true, "BackgroundMusic", _volumeOfBackgroundMusic);
    }

    public void SetSound(AudioClip clip, bool isLooped, string objectName, float volume)
    {
        void Seter(int index)
        {
            _audioDataArray[index].audioSource.clip = clip;
            _audioDataArray[index].audioSource.loop = isLooped;
            _audioDataArray[index].audioSource.volume = volume;
            _audioDataArray[index].personalVolume = volume;
            _audioDataArray[index].objectName = objectName;

            if (volume > 1)
            {
                volume = 1;
            }
            else if (volume < 0)
            {
                volume = 0;
            }

            VolumeChange();

            _audioDataArray[index].audioSource.Play();
        }

        //                                          вродь это нужно...
        //for (int i = 0; i < _sourcesCount; i++)
        //{
        //    if (!_audioDataArray[i].audioSource.isPlaying && !_audioDataArray[i].audioSource.loop && _audioDataArray[i].audioSource.clip != null)
        //    {
        //        _audioDataArray[i].audioSource.clip = null;
        //        _audioDataArray[i].objectName = null;
        //        Debug.Log("bye" + i);
        //    }
        //}

        for (int i = 0;  i < _sourcesCount; i++)
        {
            if(objectName == _audioDataArray[i].objectName && _audioDataArray[i].audioSource.clip== clip && _audioDataArray[i].audioSource.isPlaying)
            {
                return;
            }
        }

        for (int i = 0; i < _sourcesCount; i++)
        {
            if (objectName == _audioDataArray[i].objectName || _audioDataArray[i].objectName == null)
            {
                Seter(i);
                return;
            }
        }

        for (int i = 0; i < _sourcesCount; i++)
        {
            if (!_audioDataArray[i].audioSource.isPlaying && !_audioDataArray[i].audioSource.loop)
            {
                Seter(i);
                return;
            }
        }
    }

    public void VolumeChange()
    {
        for (int i = 0; i < _sourcesCount; i++)
        {
            _audioDataArray[i].audioSource.volume = _audioDataArray[i].personalVolume*VolumeSaver;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < _sourcesCount; i++)
        {
            _audioDataArray[i].audioSource.clip = null;
            _audioDataArray[i].audioSource.loop = false;
            _audioDataArray[i].objectName = null;
            _audioDataArray[i].personalVolume = 0;
            _audioDataArray[i].audioSource.volume = 0;
        }
    }
}
