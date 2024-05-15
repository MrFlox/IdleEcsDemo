using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class ParabolaDropFromPlayer : UpdateSystem
    {
        private Filter _filter;
        private Stash<ParabolaDropFromPlayerComponent> _stash;
        private Stash<TransformComponent> _positionStash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<ParabolaDropFromPlayerComponent>().With<TransformComponent>().Build();
            _stash = World.GetStash<ParabolaDropFromPlayerComponent>();
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
                t += c.Speed * deltaTime;
                _positionStash.Get(e).Transform.position = 
                    GetPointOnParabola(t, c.StartPosition.position, c.EndPositionValue, 1f);
                if (t > 1)
                {
                    t = 1;
                    c.Finished = true;
                    e.RemoveComponent<ParabolaDropFromPlayerComponent>();
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
                    c.Activated = true;
            }
        }
        
        /// <summary>
        /// Calculates a point on a parabolic curve between two points in 3D space.
        /// </summary>
        /// <param name="t">The parameter t, ranging from 0 to 1.</param>
        /// <param name="startPoint">The start point of the parabola.</param>
        /// <param name="endPoint">The end point of the parabola.</param>
        /// <param name="height">The height of the parabola at its peak (t = 0.5).</param>
        /// <returns>The position on the parabola at parameter t.</returns>
        public static Vector3 GetPointOnParabola(float t, Vector3 startPoint, Vector3 endPoint, float height = 2f)
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