using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public sealed class FaceToCameraSystem: UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _stash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<FaceToCameraComponent>().Build();
            _stash = World.GetStash<TransformComponent>();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                _stash.Get(e).Transform.LookAt(2  *_stash.Get(e).Transform.position- Camera.main.transform.position);
            }
        }
    }
}