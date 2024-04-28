using Components;
using Systems.Helpers;
using UnityEngine;

namespace Systems
{
    public static class Utils
    {
        public static bool CheckDistance(ref PositionOnStage first, ref PositionOnStage second, float radius)
        {
            return Vector2.Distance(first.Pos(), second.Pos()) < radius;
        }
    }
}