

using Features.Berries.Components;
using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Berries.Systems
{
    public class CollectBerrySystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _stash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<CollectableBerryComponent>().With<TransformComponent>().Build();
            _stash = World.GetStash<TransformComponent>();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();

            foreach (var e in _filter)
            {
                ref var transform = ref _stash.Get(e);
                if (!Utils.CheckDistance(ref transform, ref playerTransform, .2f)) continue;
                var obj = e.GetComponent<TransformComponent>().Transform.gameObject;
                Object.Destroy(obj);
                
                e.RemoveComponent<CollectableBerryComponent>();
            }
        }
    }
}