using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonsScript : MonoBehaviour
{
    //все последующие сцены должны быть тоже игровыми
    [SerializeField]private SerializeField _firstGameplayScene;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private Transform _canvasTransform;
    [SerializeField] private Vector2[] nums;

    private void Awake()
    {

        Vector2 spawnPoint = new(0, 0);
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            Instantiate(_playButton, spawnPoint, Quaternion.identity, _canvasTransform.transform);

        }
    }
}
