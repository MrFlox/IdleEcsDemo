using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class LootRotationSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<LootRotationComponent> _stash;
        private Stash<TransformComponent> _positionStash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<LootRotationComponent>().With<TransformComponent>().Build();
            _stash = World.GetStash<LootRotationComponent>();
            _positionStash = World.GetStash<TransformComponent>();
        }
        
        private void ActivateLoot()
        {
            foreach (var e in _filter)
            {
                ref var c = ref _stash.Get(e);
                if (c.Activated ) continue;
                _positionStash.Get(e).Transform.GetChild(0).rotation = Quaternion.AngleAxis(c.Angle, Vector3.forward);
                c.Activated = true;
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            ActivateLoot();
            
            foreach (var e in _filter)
            {
                ref var c = ref _stash.Get(e);
                if(!c.Activated) continue;
                _positionStash.Get(e).Transform.Rotate(new Vector3(0, c.Speed * deltaTime, 0));
            }
        }
    }
}