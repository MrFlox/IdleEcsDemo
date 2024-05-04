using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class ShadowUpdateSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _stash;
        private Stash<ShadowComponent> _shadows;

        public override void OnAwake()
        {
            _filter = World.Filter.With<ShadowComponent>().Build();
            _stash = World.GetStash<TransformComponent>();
            _shadows = World.GetStash<ShadowComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                ref var pos = ref _shadows.Get(e).Transform;
                if (pos == null)
                {
                    e.RemoveComponent<ShadowComponent>();
                    Object.Destroy(_stash.Get(e).Transform.gameObject);
                    continue;
                }
                _stash.Get(e).Transform.position = new Vector3(pos.position.x, .3f, pos.position.z);
            }
        }
    }
}