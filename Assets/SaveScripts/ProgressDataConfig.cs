using UnityEngine;

namespace Save_files.Scripts
{
    [CreateAssetMenu(fileName = "new Progress Data", menuName = "ProgressData", order = 1)]
    public class ProgressDataConfig : ScriptableObject
    {
        public static string DefaultConfigPatch = "/Configs/DefaultConfig.asset";
        
        [SerializeField] private ProgressData _progressData;

        public ProgressData Data
        {
            get => new ProgressData(_progressData);
        }
        
#if UNITY_EDITOR
        [ContextMenu("Debug Data")]
        public void DebugUpdateData()
        {
            Saver.DebugUpdateData(_progressData);
        }
#endif
    }
}
