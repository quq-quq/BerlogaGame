using UnityEngine;

namespace Core.Extension
{
    public static class TransformExtension
    {
        public static void SetActiveForChildren(this Transform transform, bool active)
        {
            var count = transform.childCount;
            for (var i = 0; i < count; i++)
            {
                transform.SetActiveForChild(i, active);
            }
        }

        public static void SetActiveForChild(this Transform transform, int index, bool active)
        {
            transform.GetChild(index).gameObject.SetActive(active);
        }
    }
}