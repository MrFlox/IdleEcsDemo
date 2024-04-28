using Components;
using Generators.Components;
using Generators.Providers;
using Player.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using static Systems.Utils;

namespace Generators.Systems
{
    public class GeneratrosActivatorSystem: UpdateSystem
    {
        private Filter _generatorsFilter;
        private Filter _playersFilter;
        private Stash<ResourceGeneratorComponent> _resourceGeneratorComponentStash;
        private Entity _player;

        public override void OnAwake()
        {
            _generatorsFilter = World.Filter.With<GeneratorComponent>().With<ResourceGeneratorComponent>().Build();
            _playersFilter = World.Filter.With<PlayerComponent>().With<PositionOnStage>().Build();
            _resourceGeneratorComponentStash = World.GetStash<ResourceGeneratorComponent>();
            _player = _playersFilter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerTransform = ref _player.GetComponent<PositionOnStage>();
            foreach (var entity in _generatorsFilter)
                UpdateGenerator(entity, playerTransform);
        }

        private void UpdateGenerator(Entity entity, PositionOnStage playerTransform)
        {
            ref var resourceComponent = ref _resourceGeneratorComponentStash.Get(entity);
            SetGeneratorState(
                playerTransform, 
                ref entity.GetComponent<RadiusColliderComponent>(),
                ref entity.GetComponent<PositionOnStage>(), entity);
        }
        
        private void SetGeneratorState(PositionOnStage playerTransform,
            ref RadiusColliderComponent radius,
            ref PositionOnStage position, Entity entity)
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