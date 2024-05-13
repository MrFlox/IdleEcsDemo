using Features.CollectingPoint.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;

namespace Features.CollectingPoint.Systems
{
    public class CollectingPointActivationSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
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
                if (CheckDistanceWithPlayer(e, 3.4f))
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