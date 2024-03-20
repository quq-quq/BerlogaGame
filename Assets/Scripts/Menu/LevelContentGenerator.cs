using System;
using Menu;
using Save_files.Scripts;
using UnityEngine;

public class LevelContentGenerator : MonoBehaviour
{  
    
    [SerializeField] private LevelButton _levelButtonPrefab;
    [SerializeField] private LevelContainer _levelPrefab;
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
        var enable = true;
        for (var i = 0; i < levels.Count; i++)
        {
            var levelGO = Instantiate(_levelPrefab, gameObject.transform);
            levelGO.Text = "Уровень " + (i + 1);
            var level = levels[i];
            for (int j = 0; j < level.Parts.Count; j++)
            {
                var levelButton = Instantiate(_levelButtonPrefab, levelGO.Container);
                levelButton.Init(level.Parts[j], j+1, enable);
                if (!level.Parts[j].IsCompleted)
                {
                    enable = false;
                }
            }
        }
    }
}
