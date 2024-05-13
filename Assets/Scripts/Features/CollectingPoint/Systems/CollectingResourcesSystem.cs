using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.CollectingPoint.Systems
{
    public class CollectingResourcesSystem : UpdateSystem
    {
        private const float CollectResourceRadius = .2f;
        
        private Filter _filter;
        private Stash<CollectableResourceComponent> _stash;
        public override void OnAwake() => _filter = World.Filter.With<CollectableResourceComponent>().Build();

        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();

            foreach (var e in _filter)
            {
                if (Utils.CheckDistance(ref playerTransform, ref e.GetComponent<TransformComponent>(), CollectResourceRadius))
                {
                    var obj = e.GetComponent<TransformComponent>().Transform.gameObject;
                    Object.Destroy(obj);
                    e.RemoveComponent<CollectableResourceComponent>();
                }
            }
        }
    }
}