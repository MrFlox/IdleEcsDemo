using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Systems
{
    public class BallGeneratorSystem : UpdateSystem
    {
        private Filter _ballGeneratorFilter;
        private Stash<BallsGeneratorComponent> _moveStash;
        private Filter _playerFilter;
        
        public override void OnAwake()
        {
            _ballGeneratorFilter = World.Filter.With<BallsGeneratorComponent>().With<PositionOnStage>().Build();
            _playerFilter = World.Filter.With<Player>().Build();
            
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (var ballGenEntity in _ballGeneratorFilter)
            {
                if (InRange(GetEntityPos(ballGenEntity), GetPlayerPos()))
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
        
        private bool InRange(Vector3 getEntityPos, Vector3 getPlayerPos)
        {
            var dist = Vector2.Distance(new Vector2(getEntityPos.x, getEntityPos.z), new Vector2(getPlayerPos.x, getEntityPos.z));
           return dist < 3f;
        }
        
        private Vector3 GetPlayerPos() => _playerFilter.First().GetComponent<PositionOnStage>().Transform.position;
        
        private Vector3 GetEntityPos(Entity ballGenEntity) => ballGenEntity.GetComponent<PositionOnStage>().Transform.position;

        
    }
}