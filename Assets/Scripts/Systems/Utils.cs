using Components;
using Systems.Helpers;

namespace Systems
{
    public static class Utils
    {
        public static bool CheckDistance(ref PositionOnStage first, ref PositionOnStage second, float radius)
        {
            var difference = first.Pos() - second.Pos();
            var distanceSquared = difference.sqrMagnitude;
            var radiusSquared = radius * radius;
            return distanceSquared <= radiusSquared;
        }
    }
}