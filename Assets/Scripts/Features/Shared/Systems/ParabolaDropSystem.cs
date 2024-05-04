using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class ParabolaDropSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<ParabolaDropComponent> _stash;
        private Stash<TransformComponent> _positionStash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<ParabolaDropComponent>().With<TransformComponent>().Build();
            _stash = World.GetStash<ParabolaDropComponent>();
            _positionStash = World.GetStash<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ActivateDrops();
            foreach (var e in _filter)
            {
                ref var c = ref _stash.Get(e);

                if (c.Finished) continue;
                ref var t = ref c.Time;
                _positionStash.Get(e).Transform.position = GetPointOnParabola(t, c.StartPosition, c.EndPosition, 2f);
                t += c.Speed * deltaTime;
                if (t > 1)
                {
                    t = 1;
                    c.Finished = true;
                    
                    // AddRotationComponent(e);

                    e.RemoveComponent<ParabolaDropComponent>();
                }
                if (t < 0) t = 0;
            }
        }
        
        private void ActivateDrops()
        {
            foreach (var e in _filter)
            {
                ref var c = ref _stash.Get(e);
                if (!c.Activated)
                {
                    c.Activated = true;
                    c.StartPosition = _positionStash.Get(e).Transform.position;
                    c.EndPosition = GetRandomPoint(c.StartPosition, 2f);
                }
            }
        }

        private static void AddRotationComponent(Entity e)
        {
            ref var newC = ref e.AddComponent<LootRotationComponent>();
            newC.Angle = 30;
            newC.Speed = 25;
        }


        /// <summary>
        /// Returns a random point on a circle in the XZ plane around a given center point.
        /// </summary>
        /// <param name="center">The center point of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>A random point on the circumference of the circle.</returns>
        public static Vector3 GetRandomPoint(Vector3 center, float radius)
        {
            // Generate a random angle between 0 to 360 degrees (0 to 2*PI radians)
            float angle = Random.Range(0f, Mathf.PI * 2);

            // Calculate the x and z coordinates
            float x = center.x + radius * Mathf.Cos(angle);
            float z = center.z + radius * Mathf.Sin(angle);

            // Return the Vector3 point on the circle in the XZ plane
            return new Vector3(x, center.y, z);
        }

        /// <summary>
        /// Calculates a point on a parabolic curve between two points in 3D space.
        /// </summary>
        /// <param name="t">The parameter t, ranging from 0 to 1.</param>
        /// <param name="startPoint">The start point of the parabola.</param>
        /// <param name="endPoint">The end point of the parabola.</param>
        /// <param name="height">The height of the parabola at its peak (t = 0.5).</param>
        /// <returns>The position on the parabola at parameter t.</returns>
        public static Vector3 GetPointOnParabola(float t, Vector3 startPoint, Vector3 endPoint, float height = 5f)
        {
            // Linearly interpolate between start and end points to find the base position
            Vector3 basePosition = Vector3.Lerp(startPoint, endPoint, t);

            // Find the direction perpendicular to the line between start and end
            Vector3 direction = (endPoint - startPoint).normalized;

            // Calculate height using the inverted parabola formula adjusted to Unity's coordinate system
            float parabolicT = 2 * (t - 0.5f);
            float verticalOffset = -parabolicT * parabolicT + 1;

            // The peak of the parabola is at t = 0.5, and falls off towards the start/end points
            Vector3 heightVector = new Vector3(0, height * verticalOffset, 0);

            // Add height to the base position
            return basePosition + heightVector;
        }
    }


}