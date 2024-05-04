using Features.Berries.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Berries.Systems
{
    public class DeleteBerriesSystem: UpdateSystem 
    {
        private Filter _filter;
        private Stash<TransformComponent> _stash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<DeleteComponent>().Build();
            _stash = World.GetStash<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                var gameObject = _stash.Get(entity).Transform.gameObject;
                Object.Destroy(gameObject);
                World.RemoveEntity(entity);
            }
        }
    }
}