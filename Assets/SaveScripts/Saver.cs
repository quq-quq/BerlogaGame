using UnityEditor;
using UnityEngine;
using File = System.IO.File;

namespace Save_files.Scripts
{
    public static class Saver
    {
        private static readonly string Path =  "save";
        private static ProgressData _data;

        public static ProgressData Data
        {
            get
            {
                if(_data == null)
                {
                    Load();
                }
                return _data;
            }

            private set => _data = value;
        }
        
        
        public static void Load()
        {
            if(!File.Exists(Path))
            {
                Data = Resources.Load<ProgressDataConfig>(ProgressDataConfig.DefaultConfigPatch).Data;
                return;
            }
            
            var json = File.ReadAllText(Path);
            Data = JsonUtility.FromJson<ProgressData>(json);
        }

        public static void DeleteSaves()
        {
            var isMute = Data.IsMute;
            Data = Resources.Load<ProgressDataConfig>(ProgressDataConfig.DefaultConfigPatch).Data;
            Data.IsMute = isMute;
            var json = JsonUtility.ToJson(Data);
            File.WriteAllText(Path,json);
        }
        
        public static void Save()
        {
            var json = JsonUtility.ToJson(Data);
            File.WriteAllText(Path,json);
        }

#if UNITY_EDITOR
        public static void DebugUpdateData(ProgressData data)
        {
            var json = JsonUtility.ToJson(data);
            File.WriteAllText(Path, json);
        }
#endif
    }
}
