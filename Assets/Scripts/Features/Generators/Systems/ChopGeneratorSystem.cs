using Features.Berries.Components;
using Features.FloatingObjects.Components;
using Features.Generators.Components;
using Features.Player.Components;
using Features.Shared.Components;
using Features.Shared.Providers;
using Features.Shared.Systems;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;
using static UnityEngine.Object;

namespace Features.Generators.Systems
{
    public sealed class ChopGeneratorSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private Filter _filter;
        private GameSettings _settings;

        public ChopGeneratorSystem(GameSettings settings) => _settings = settings;

        public override void OnAwake()
        {
            base.OnAwake();
            _filter = World.Filter.With<ChopGeneratorComponent>().With<TransformComponent>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();

            foreach (var e in _filter)
            {
                GetTransformComponent(e, out var transform);
                if (!CheckDistanceWithPlayer(e, 1)) continue;
                AddResourcesOnStage(transform.Transform.position, ref playerTransform.Transform);
                RemoveResourceBase(transform);

                e.RemoveComponent<ChopGeneratorComponent>();
            }
        }

        private void RemoveResourceBase(TransformComponent transform) =>
            Destroy(transform.Transform.gameObject);

        private void AddResourcesOnStage(Vector3 transform, ref Transform player)
        {
            for (int i = 0; i < 5; i++)
            {
                var ball = Instantiate(_settings.ResBall);
                ball.transform.position = transform;

                AddFloatingComponent(ball, ref player);
            }
        }
        private void AddFloatingComponent(GameObject ball, ref Transform player)
        {
            var entity = ball.GetComponent<TransformProvider>().Entity;
            ref var c = ref entity.AddComponent<FloatingComponent>();
            c.InitialY = entity.GetComponent<TransformComponent>().Transform.localPosition.y;
            c.MoveSpeed = Random.Range(.2f, .6f);
            c.Direction = (FloatingComponent.MoveDirection)Random.Range(0, 1);

            entity.AddComponent<CollectableResourceComponent>().CollectorEntity = Player;
            var moveToTransformComponent = entity.AddComponent<MoveToTransformComponent>();
            moveToTransformComponent.Target = player.transform;
            moveToTransformComponent.Speed = 3;
            moveToTransformComponent.Accel = 1.2f;

            var shadow = Instantiate(_settings.Shadow);
            var shEntity = shadow.GetComponent<ShadowProvider>().Entity;

            shEntity.GetComponent<ShadowComponent>().Transform = ball.transform;
        }
    }
}