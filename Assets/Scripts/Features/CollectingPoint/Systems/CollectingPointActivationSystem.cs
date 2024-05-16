using Features.CollectingPoint.Components;
using Features.Shared.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;

namespace Features.CollectingPoint.Systems
{
    public class CollectingPointActivationSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private const float DefaultRadius = 3.5f;
        private Filter _filter;
        
        public override void OnAwake()
        {
            base.OnAwake();
            _filter = World.Filter.With<CollectingPointComponent>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            
            foreach (var e in _filter)
            {
                var radiusComp = e.GetComponent<RadiusColliderComponent>(out var isExist);
                var radius = DefaultRadius;
                
                if (isExist)
                    radius = radiusComp.Radius;
                
                if (CheckDistanceWithPlayer(e, radius))
                {
                    if (!e.Has<CollectingPointActivatedComponent>())
                        e.AddComponent<CollectingPointActivatedComponent>();
                }
                else
                {
                    if (e.Has<CollectingPointActivatedComponent>())
                        e.RemoveComponent<CollectingPointActivatedComponent>();
                }
            }
        }
    }
}