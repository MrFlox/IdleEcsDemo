using Features.Shared.Components;
using Scellecs.Morpeh;
using ScriptableObjects;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class MoveToTransformSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private Filter _filter;
        private Stash<MoveToTransformComponent> _moveToStash;
        private GameSettings _setting;
        
        public MoveToTransformSystem(GameSettings setting) => _setting = setting;

        public override void OnAwake()
        {
            base.OnAwake();
            _filter = World.Filter.With<MoveToTransformComponent>().With<TransformComponent>().Build();
            _moveToStash = World.GetStash<MoveToTransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                if (CheckDistanceWithPlayer(e, 2f))
                {
                    GetTransformComponent(e, out var transform);
                    ref var accel = ref _moveToStash.Get(e).Accel;
                    transform.Transform.position =
                        Vector3.MoveTowards(transform.Transform.position, PlayerTransform.Transform.position,
                            _setting.BerryCollectableSpeed * deltaTime);
                    accel *= 1.2f;
                }
            }
        }
    }
}