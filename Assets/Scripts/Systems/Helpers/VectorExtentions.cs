using UnityEngine;

namespace Systems.Helpers
{
    public static class VectorExtentions
    {
        public static Vector2 XZVector(this Vector3 vector) => new(vector.x, vector.z);
    }
}