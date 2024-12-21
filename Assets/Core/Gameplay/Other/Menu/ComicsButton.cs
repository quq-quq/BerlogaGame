using Core.Gameplay.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ComicsButton : MonoBehaviour
{
    [SerializeField] private string _comicName;
    [SerializeField] private Button _button;
    [SerializeField] private SceneData _comicsScene;
    [SerializeField] private GameObject _blockPanel;
    [SerializeField] private bool _overrideOpen = false;
    
    private bool _opened = false;
    private SceneLoader _sceneLoader;

    [Inject]
    private void Inject(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }
    
    private void Awake()
    {
        _opened = PlayerPrefs.GetInt(_comicName, 0) != 0 || _overrideOpen;
        _blockPanel.SetActive(!_opened);
    }

    private void OnEnable()
    { 
        if(_opened)
            _button.onClick.AddListener(OpenComics);   
    }

    private void OnDisable()
    {
        if(_opened)
            _button.onClick.RemoveListener(OpenComics);   
    }

    private void OpenComics()
    {
        _sceneLoader.LoadScene(_comicsScene);
    }
}
