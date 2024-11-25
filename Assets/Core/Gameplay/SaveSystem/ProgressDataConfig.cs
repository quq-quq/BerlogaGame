using UnityEngine;

namespace Save_files.Scripts
{
    [CreateAssetMenu(fileName = "new Progress Data", menuName = "ProgressData", order = 1)]
    public class ProgressDataConfig : ScriptableObject
    {
        public static string DefaultConfigPatch = "Assets/Core/Data/Save/DefaultConfig.asset";
        
        [SerializeField] private ProgressData _progressData;

        public ProgressData Data => new(_progressData);
        
#if UNITY_EDITOR
        [ContextMenu("Debug Data")]
        public void DebugUpdateData()
        {
            Saver.DebugUpdateData(_progressData);
        }
#endif
    }
}
