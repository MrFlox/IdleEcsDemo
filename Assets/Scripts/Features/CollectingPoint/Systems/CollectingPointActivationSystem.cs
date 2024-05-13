using Features.CollectingPoint.Components;
using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.CollectingPoint.Systems
{
    public class CollectingPointActivationSystem:UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _collectingPoints;
        public override void OnAwake()
        {
            _filter = World.Filter.With<CollectingPointComponent>().Build();
            _collectingPoints = World.GetStash<TransformComponent>();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();
            
            foreach (var e in _filter)
            {
                if (Utils.CheckDistance(ref _collectingPoints.Get(e), ref playerTransform, 3.4f))
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