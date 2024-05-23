using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class FlyingUpLabelSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _transformStash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<FlyingUpLabelComponent>().With<TransformComponent>().Build();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                _transformStash.Get(e).Transform.Translate(Vector3.up * deltaTime * 3);

                if (_transformStash.Get(e).Transform.position.y > 3)
                {
                    Object.Destroy(_transformStash.Get(e).Transform.gameObject);
                    World.RemoveEntity(e);
                }
            }
        }

    }
}