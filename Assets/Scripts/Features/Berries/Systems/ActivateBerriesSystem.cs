using System;
using Features.Berries.Components;
using Features.Berries.Providers;
using Features.Generators.Components;
using Features.Generators.Providers;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Berries.Systems
{
    public class ActivateBerriesSystem : UpdateSystem
    {
        //todo: remake using Signals
        public event Action OnBerryActivated;
        //---
        private GameSettings _settings;
        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _activatedBerries;
        private Stash<ActivatedGenerator> _generators;

        public ActivateBerriesSystem(GameSettings settings)
        {
            _settings = settings;
        }

        public override void OnAwake()
        {
            _filter = World.Filter.With<ActivatedGenerator>().Without<GrowingBerriesComponent>().Build();
            _activatedBerries = World.GetStash<ResourceGeneratorComponent>();
            _generators = World.GetStash<ActivatedGenerator>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var berrySettings = ref _settings.BerriesSettings;
            foreach (var entity in _filter)
            {
                if (entity.Has<ActivatedGenerator>())
                {
                    ref var activatedComponent = ref _generators.Get(entity);
                    ref var lastSpawnTime = ref activatedComponent.LastSpawnTime;

                    if (Time.time - lastSpawnTime >= berrySettings.BerryFlyDelay)
                    {
                        ShowFlyingBerries(entity);
                        lastSpawnTime = Time.time;
                    }

                    if (_activatedBerries.Get(entity).Berries.Count == 0)
                    {
                        entity.RemoveComponent<GeneratorComponent>();
                        entity.RemoveComponent<ActivatedGenerator>();
                    }
                }
            }
        }

        private void ShowFlyingBerries(Entity entity)
        {
            ref var berrySettings = ref _settings.BerriesSettings;
            var berries = _activatedBerries.Get(entity).Berries;
            var berry = berries[0];
            berries.RemoveAt(0);
            var newEntity = World.CreateEntity();
            newEntity.AddComponent<PositionOnStage>().Transform = berry;
            ref var berryComponent = ref newEntity.AddComponent<BerryComponent>();
            berryComponent.Speed = berrySettings.BerryFlySpeed;
            berryComponent.Entity = newEntity;
            
            OnBerryActivated?.Invoke();
        }
    }
}