using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

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
        private Entity _player;

        public override void OnAwake()
        {
            _generatorsFilter = World.Filter.With<GeneratorComponent>().With<ResourceGeneratorComponent>().Build();
            _playersFilter = World.Filter.With<Player>().With<PositionOnStage>().Build();
            _generatorStash = World.GetStash<GeneratorComponent>();
            _player = _playersFilter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerTransform = ref _player.GetComponent<PositionOnStage>();
            foreach (var entity in _generatorsFilter)
            {
                ref var generator = ref _generatorStash.Get(entity);
                if (Vector3.Distance(playerTransform.Transform.position, generator.Transform.position) < 3)
                {
                    generator.CircleMaterial.material.color = Color.green;
                    if (entity.GetComponent<ResourceGeneratorComponent>().State !=
                        ResourceGeneratorComponent.ResourceStates.Collecting)
                    {
                        entity.GetComponent<ResourceGeneratorComponent>().State =
                            ResourceGeneratorComponent.ResourceStates.ReadyToCollect;
                    }
                }
                else
                {
                    generator.CircleMaterial.material.color = Color.red;
                }


                if (entity.GetComponent<ResourceGeneratorComponent>().State ==
                    ResourceGeneratorComponent.ResourceStates.ReadyToCollect)
                {
                    entity.GetComponent<ResourceGeneratorComponent>().State =
                        ResourceGeneratorComponent.ResourceStates.Collecting;
                    var berries = entity.GetComponent<ResourceGeneratorComponent>()._Berries;
                    
                    // foreach (var berry in berries)
                    // {
                    //     if(!berry.Entity.Has<BerryComponent>())
                    //      berry.Entity.AddComponent<BerryComponent>();
                    // }
                    entity.GetComponent<ResourceGeneratorComponent>().State =
                        ResourceGeneratorComponent.ResourceStates.Done;
                }
            }
        }
    }
}