using Features.Shared.Components;
using UnityEngine;

public static class VectorExtensions
{
    private static Vector2 XZVector(this Vector3 vector) => new(vector.x, vector.z);
    
    public static Vector2 Pos(this PositionOnStage component) => component.Transform.position.XZVector();
}