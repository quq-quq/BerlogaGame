using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Save_files.Scripts
{
    [Serializable]
    public class ProgressData
    {
        public ProgressData(ProgressData progressData)
        {
            _levels = new List<Level>();

            foreach (var progressDataLevel in progressData.Levels)
            {
                _levels.Add(new Level(progressDataLevel));
            }

            _isMute = progressData.IsMute;
            _volume = progressData.Volume;
        }
        
        [SerializeField] private List<Level> _levels;
        [SerializeField] private bool _isMute = false;
        [SerializeField, Range(0,1)] private float _volume = 1f;
        
        public bool IsMute
        {
            get => Volume==0 || _isMute;
            set => _isMute = value;
        }

        public float Volume
        {
            get => _volume;
            set => _volume = value;
        }
        
        public IReadOnlyList<Level> Levels
        {
            get => _levels;
        }
        
        public void SetCompleted(string sceneName)
        {
            foreach (var level in _levels)
            {
                foreach (var part in level.Parts)
                {
                    
                    if (sceneName == part.SceneName)
                    {
                        part.IsCompleted = true;
                        
                    }
                }
            }

            Saver.Save();
        }

        public bool IsLevelAvailableByIndex(int index)
        {
            if(index >= _levels.Count)
                return false;
            
            var r = true;
            for (var i = 0; i < index && i < _levels.Count; i++)
            {
                if(!_levels[i].IsCompleted)
                {
                    r = false;
                    break;
                }
            }
            return r;
        }
    }

    [Serializable]
    public class Level
    {
        public Level(Level level)
        {
            _parts = new List<LevelPart>();

            foreach (var levelPart in level.Parts)
            {
                _parts.Add(new LevelPart(levelPart));
            }
        }
        
        [SerializeField] private List<LevelPart> _parts;

        public IReadOnlyList<LevelPart> Parts
        {
            get => _parts;
        }

        public bool IsCompleted => _parts.All(x => x.IsCompleted);
    }
    [Serializable]
    public class LevelPart
    {
        public LevelPart(LevelPart levelPart)
        {
            _sceneName = levelPart.SceneName;
            _isCompleted = levelPart.IsCompleted;
        }
        
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _isCompleted;
        public string SceneName
        {
            get => _sceneName;
            set => _sceneName = value;
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }
    }
}
