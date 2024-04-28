using System;
using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using Systems.Helpers;
using UnityEngine;
using static Systems.Utils;

namespace Systems
{
    public class GeneratrosActivatorSystem: UpdateSystem
    {
        private Filter _generatorsFilter;
        private Filter _playersFilter;
        private Stash<GeneratorComponent> _generatorStash;
        private Stash<ResourceGeneratorComponent> _resourceGeneratorComponentStash;
        private Entity _player;
        private BerryActivator _berryActivator;
    
        public override void OnAwake()
        {
            _generatorsFilter = World.Filter.With<GeneratorComponent>().With<ResourceGeneratorComponent>().Build();
            _playersFilter = World.Filter.With<Player>().With<PositionOnStage>().Build();
            _generatorStash = World.GetStash<GeneratorComponent>();
            _resourceGeneratorComponentStash = World.GetStash<ResourceGeneratorComponent>();
            _player = _playersFilter.First();

            _berryActivator = new BerryActivator(World);
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerTransform = ref _player.GetComponent<PositionOnStage>();
            foreach (var entity in _generatorsFilter)
                UpdateGenerator(entity, playerTransform);
        }

        private void UpdateGenerator(Entity entity, PositionOnStage playerTransform)
        {
            ref var generator = ref _generatorStash.Get(entity);
            ref var resourceComponent = ref _resourceGeneratorComponentStash.Get(entity);
            SetGeneratorState(
                playerTransform, 
                ref resourceComponent, 
                ref entity.GetComponent<RadiusColliderComponent>(),
                ref entity.GetComponent<PositionOnStage>());

            CheckGeneratorState(resourceComponent, entity);
        }
        
        private void SetGeneratorState(PositionOnStage playerTransform,
            ref ResourceGeneratorComponent resourceComponent, ref RadiusColliderComponent radius,
            ref PositionOnStage position)
        {
            if (CheckDistance(ref playerTransform, ref position, radius.Radius))
            {
                _berryActivator.ActivateGenerator(ref resourceComponent);
            }
        }

        private void CheckGeneratorState(ResourceGeneratorComponent resourceComponent, Entity entity)
        {
            if (resourceComponent.State == ResourceGeneratorComponent.ResourceStates.ReadyToCollect)
            {
                resourceComponent.State = ResourceGeneratorComponent.ResourceStates.Collecting;
                _berryActivator.ActivateBerries(resourceComponent);
                resourceComponent.State = ResourceGeneratorComponent.ResourceStates.Done;
                entity.RemoveComponent<ResourceGeneratorComponent>();
            }
        }
    }
}