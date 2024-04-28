using Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UpdateSystem = Scellecs.Morpeh.Addons.Systems.UpdateSystem;


namespace Systems
{
    public sealed class FloatingInitializer : UpdateSystem
    {

        private Filter _filter;
        private Stash<FloatingComponent> _moveStash;
        private Stash<PositionOnStage> _positionsStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<FloatingComponent>().With<PositionOnStage>().Build();
            _moveStash = World.GetStash<FloatingComponent>();
            _positionsStash = World.GetStash<PositionOnStage>();

            foreach (var entity in _filter)
            {
                ref var floatingObject = ref _moveStash.Get(entity);
                floatingObject.InitialY = _positionsStash.Get(entity).Transform.localPosition.y;
                floatingObject.MoveSpeed = Random.Range(.2f, .6f);
                floatingObject.Direction = (FloatingComponent.MoveDirection)Random.Range(0, 1);
            }
        }
        public override void OnUpdate(float deltaTime)
        {
        }

        public override void Dispose()
        {
        }
    }
}