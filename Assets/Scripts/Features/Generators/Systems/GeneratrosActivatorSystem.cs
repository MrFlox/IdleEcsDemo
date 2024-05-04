using Features.Berries.Components;
using Features.Generators.Components;
using Features.Generators.Providers;
using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using static Utils;

namespace Features.Generators.Systems
{
    public class GeneratrosActivatorSystem: UpdateSystem
    {
        private Filter _generatorsFilter;
        private Filter _playersFilter;
        private Stash<ResourceGeneratorComponent> _resourceGeneratorComponentStash;
        private Entity _player;

        public override void OnAwake()
        {
            _generatorsFilter = World.Filter.With<GeneratorComponent>().With<ResourceGeneratorComponent>().Without<GrowingBerriesComponent>().Build();
            _playersFilter = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build();
            _resourceGeneratorComponentStash = World.GetStash<ResourceGeneratorComponent>();
            _player = _playersFilter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerTransform = ref _player.GetComponent<TransformComponent>();
            foreach (var entity in _generatorsFilter)
                UpdateGenerator(entity, playerTransform);
        }

        private void UpdateGenerator(Entity entity, TransformComponent playerTransform)
        {
            ref var resourceComponent = ref _resourceGeneratorComponentStash.Get(entity);
            SetGeneratorState(
                playerTransform, 
                ref entity.GetComponent<RadiusColliderComponent>(),
                ref entity.GetComponent<TransformComponent>(), entity);
        }
        
        private void SetGeneratorState(TransformComponent playerTransform,
            ref RadiusColliderComponent radius,
            ref TransformComponent position, Entity entity)
        {
            if (CheckDistance(ref playerTransform, ref position, radius.Radius))
            {
                if(!entity.Has<ActivatedGenerator>())
                    entity.AddComponent<ActivatedGenerator>();
            }
            else
            {
                if (entity.Has<ActivatedGenerator>())
                    entity.RemoveComponent<ActivatedGenerator>();
            }
        }
    }
}