using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class MoveToTransformSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<MoveToTransformComponent> _moveToStash;
        private Stash<TransformComponent> _transformStash;
        private GameSettings _setting;
        
        public MoveToTransformSystem(GameSettings setting)
        {
            _setting = setting;
        }

        public override void OnAwake()
        {
            _filter = World.Filter.With<MoveToTransformComponent>().With<TransformComponent>().Build();
            _moveToStash = World.GetStash<MoveToTransformComponent>();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();
            foreach (var e in _filter)
            {
                if (Utils.CheckDistance(ref _transformStash.Get(e), ref playerTransform, 2f))
                {
                    ref var transform = ref _transformStash.Get(e);
                    ref var accel = ref _moveToStash.Get(e).Accel;
                    transform.Transform.position =
                        Vector3.MoveTowards(transform.Transform.position, playerTransform.Transform.position,
                            _setting.BerryCollectableSpeed * deltaTime);
                    accel *= 1.2f;

                }
            }
        }
    }
}