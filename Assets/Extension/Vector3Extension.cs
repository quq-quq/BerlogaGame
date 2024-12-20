using UnityEngine;

namespace Core.Extension
{
    public static class Vector3Extension
    {
        public static Vector3 ClearZ(this Vector3 vector) => new (vector.x, vector.y, 0);
        public static Vector3 ClearY(this Vector3 vector) => new (vector.x, 0, vector.z);
        public static Vector3 ClearX(this Vector3 vector) => new (0, vector.y, vector.z);
    }
}