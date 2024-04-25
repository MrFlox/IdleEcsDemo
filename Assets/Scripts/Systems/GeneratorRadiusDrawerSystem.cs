using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static Components.ResourceGeneratorComponent;

namespace Systems
{


    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(GeneratorRadiusDrawerSystem))]
    public sealed class GeneratorRadiusDrawerSystem : UpdateSystem
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
            
            // World.Commit();
        }

        private void UpdateGenerator(Entity entity, PositionOnStage playerTransform)
        {
            ref var generator = ref _generatorStash.Get(entity);
            ref var resourceComponent = ref _resourceGeneratorComponentStash.Get(entity);
            SetGeneratorState(playerTransform, generator, ref resourceComponent);

            CheckGeneratorState(resourceComponent, entity);
        }

        private  void SetGeneratorState(PositionOnStage playerTransform,
            GeneratorComponent generator, ref ResourceGeneratorComponent resourceComponent)
        {
            if (Vector3.Distance(playerTransform.Transform.position, generator.Transform.position) < 3)
                _berryActivator.ActivateGenerator(generator, ref resourceComponent);
            else
                _berryActivator.DeactivateGenerator(generator);
        }

        private void CheckGeneratorState(ResourceGeneratorComponent resourceComponent, Entity entity)
        {
            if (resourceComponent.State == ResourceStates.ReadyToCollect)
            {
                resourceComponent.State = ResourceStates.Collecting;
                _berryActivator.ActivateBerries(resourceComponent);
                resourceComponent.State = ResourceStates.Done;
                entity.RemoveComponent<ResourceGeneratorComponent>();
            }
        }
    }
}