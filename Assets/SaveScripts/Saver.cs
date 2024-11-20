using UnityEngine;
using UnityEngine.AddressableAssets;
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
                Data = LoadDefaultConfig();
                return;
            }
            
            var json = File.ReadAllText(Path);
            Data = JsonUtility.FromJson<ProgressData>(json);
        }

        public static void DeleteSaves()
        {
            var isMute = Data.IsMute;
            Data = LoadDefaultConfig();
            Data.IsMute = isMute;
            var json = JsonUtility.ToJson(Data);
            File.WriteAllText(Path,json);
        }
        
        private static ProgressData LoadDefaultConfig()
        => Addressables.LoadAssetAsync<ProgressDataConfig>(ProgressDataConfig.DefaultConfigPatch).WaitForCompletion().Data;
        
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
