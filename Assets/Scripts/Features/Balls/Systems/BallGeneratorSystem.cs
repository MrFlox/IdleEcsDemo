using Features.Balls.Components;
using Features.Shared.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace Features.Balls.Systems
{
    public class BallGeneratorSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private Filter _generatorFilter;
        private Stash<RadiusColliderComponent> _rads;

        public override void OnAwake()
        {
            base.OnAwake();
            _generatorFilter = World.Filter.With<BallsGeneratorComponent>().With<TransformComponent>().Build();
            _rads = World.GetStash<RadiusColliderComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _generatorFilter)
            {
                var radius = _rads.Get(e).Radius;
                
                if (CheckDistanceWithPlayer(e, radius))
                {
                    if (!e.Has<SpawningBalls>())
                        e.AddComponent<SpawningBalls>().LastSpawnTime = Time.time;
                }
                else
                {
                    if (e.Has<SpawningBalls>())
                        e.RemoveComponent<SpawningBalls>();
                }
            }
        }
    }
}