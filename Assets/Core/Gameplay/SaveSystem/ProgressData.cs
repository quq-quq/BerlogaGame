using System;
using System.Collections.Generic;
using System.Linq;
using Core.Gameplay.SceneManagement;
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
            get => Volume==0?true : _isMute;
            set => _isMute = value;
        }

        public float Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                if (value >= 0)
                {
                    
                }
            }
        }
        
        public IReadOnlyList<Level> Levels
        {
            get => _levels;
        }
        
        public void SetCompleted(SceneData sceneData)
        {
            foreach (var level in _levels)
            {
                foreach (var part in level.Parts)
                {
                    if (sceneData == part.SceneData)
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
            _sceneData = levelPart.SceneData;
            _isCompleted = levelPart.IsCompleted;
        }
        
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private bool _isCompleted;
        public SceneData SceneData
        {
            get => _sceneData;
            set => _sceneData = value;
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }
    }
}
