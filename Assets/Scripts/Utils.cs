using Features.Shared.Components;

public static class Utils
{
    public static bool CheckDistance(ref TransformComponent first, ref TransformComponent second, float radius)
    {
        var difference = first.Pos() - second.Pos();
        var distanceSquared = difference.sqrMagnitude;
        var radiusSquared = radius * radius;
        return distanceSquared <= radiusSquared;
    }
}