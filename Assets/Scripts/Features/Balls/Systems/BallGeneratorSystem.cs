using Features.Balls.Components;
using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Balls.Systems
{
    public class BallGeneratorSystem : UpdateSystem
    {
        private Filter _generatorFilter;
        private Filter _playerFilter;
        private Stash<PositionOnStage> _positions;
        private Stash<RadiusColliderComponent> _rads;

        public override void OnAwake()
        {
            _generatorFilter = World.Filter.With<BallsGeneratorComponent>().With<PositionOnStage>().Build();
            _playerFilter = World.Filter.With<PlayerComponent>().Build();
            
            _positions = World.GetStash<PositionOnStage>();
            _rads = World.GetStash<RadiusColliderComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerPos = ref _positions.Get(_playerFilter.First());
            foreach (var ballGenEntity in _generatorFilter)
            {
                ref var ballPos = ref _positions.Get(ballGenEntity);
                var radius = _rads.Get(ballGenEntity).Radius;
                
                if (Utils.CheckDistance(ref ballPos, ref playerPos, radius))
                {
                    if (!ballGenEntity.Has<SpawningBalls>())
                        ballGenEntity.AddComponent<SpawningBalls>().LastSpawnTime = Time.time;
                }
                else
                {
                    if (ballGenEntity.Has<SpawningBalls>())
                        ballGenEntity.RemoveComponent<SpawningBalls>();
                }
            }
        }
    }
}