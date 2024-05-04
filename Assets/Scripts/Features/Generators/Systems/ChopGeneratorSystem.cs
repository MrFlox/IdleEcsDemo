using Features.FloatingObjects.Components;
using Features.Generators.Components;
using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;
using static UnityEngine.Object;

namespace Features.Generators.Systems
{
    public sealed class ChopGeneratorSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _stash;
        private GameSettings _settings;

        public ChopGeneratorSystem(GameSettings settings) => _settings = settings;

        public override void OnAwake()
        {
            _filter = World.Filter.With<ChopGeneratorComponent>().With<TransformComponent>().Build();
            _stash = World.GetStash<TransformComponent>();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();
            
            foreach (var e in _filter)
            {
                ref var transform = ref _stash.Get(e);
                if (!Utils.CheckDistance(ref transform, ref playerTransform, 1)) continue;
                AddResourcesOnStage(transform.Transform.position);
                RemoveResourceBase(transform);
                    
                e.RemoveComponent<ChopGeneratorComponent>();
            }
        }
        
        private void RemoveResourceBase(TransformComponent transform) =>
            Destroy(transform.Transform.gameObject);

        private void AddResourcesOnStage(Vector3 transform)
        {
            for (int i = 0; i < 5; i++)
            {
                var ball = Instantiate(_settings.ResBall);
                ball.transform.position = transform;
                
                AddFloatingComponent(ball);
            }
        }
        private static void AddFloatingComponent(GameObject ball)
        {
            var entity = ball.GetComponent<TransformProvider>().Entity;
            ref var c = ref entity.AddComponent<FloatingComponent>();
            c.InitialY = entity.GetComponent<TransformComponent>().Transform.localPosition.y;
            c.MoveSpeed = Random.Range(.2f, .6f);
            c.Direction = (FloatingComponent.MoveDirection)Random.Range(0, 1);
        }
    }
}