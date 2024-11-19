using System.IO;

namespace Core.Editor
{
    public static class PathContainer
    {
        public static readonly string DataPath = "Assets/Core/Data";
        public static readonly string SceneDataPath = Path.Combine(DataPath, "SceneDatas");
    }
}