using Features.FloatingObjects.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.FloatingObjects.Systems
{
    public sealed class FloatingInitializer : Initializer
    {
        private Filter _filter;
        private Stash<FloatingComponent> _moveStash;
        private Stash<TransformComponent> _positionsStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<FloatingComponent>().With<TransformComponent>().Build();
            _moveStash = World.GetStash<FloatingComponent>();
            _positionsStash = World.GetStash<TransformComponent>();

            foreach (var entity in _filter)
            {
                ref var floatingObject = ref _moveStash.Get(entity);
                floatingObject.InitialY = _positionsStash.Get(entity).Transform.localPosition.y;
                floatingObject.MoveSpeed = Random.Range(.2f, .6f);
                floatingObject.Direction = (FloatingComponent.MoveDirection)Random.Range(0, 1);
            }
        }
    }
}