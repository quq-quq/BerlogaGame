using System;
using Menu;
using Save_files.Scripts;
using UnityEngine;

public class LevelContentGenerator : MonoBehaviour
{  
    
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private float _oneLevelHeight;
    [SerializeField] private float _offSetForEachLevel;
    [SerializeField] private float _offSetEnd;
    [SerializeField, Min(1)] private int _columCount;
    private RectTransform MyRect => (transform as RectTransform);
    private void Start()
    {
        var levels = Saver.Data.Levels;
        var newHeight =
            Mathf.Ceil(levels.Count / _columCount) * (_oneLevelHeight + _offSetForEachLevel) + _offSetEnd;
        MyRect.sizeDelta = new Vector2(MyRect.sizeDelta.x, newHeight);
        for (var i = 0; i < levels.Count; i++)
        {
            var level = levels[i];
            var levelButton = Instantiate(_levelButtonPrefab, gameObject.transform);
            levelButton.Init(level, i+1);
        }
    }
}
