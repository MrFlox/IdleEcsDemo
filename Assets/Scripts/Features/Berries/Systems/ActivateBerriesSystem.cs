using System;
using Features.Berries.Components;
using Features.Generators.Components;
using Features.Generators.Providers;
using Features.Player.Components;
using Features.Shared.Components;
using Features.Shared.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

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
        private Stash<TimingComponent> _timingComponentsStash;
        private Stash<ParabolaDropFromPlayerComponent> _parabolaComponets;
        
        public ActivateBerriesSystem(GameSettings settings)
        {
            _settings = settings;
        }

        public override void OnAwake()
        {
            _filter = World.Filter.With<ActivatedGenerator>().Without<GrowingBerriesComponent>().Build();
            _activatedBerries = World.GetStash<ResourceGeneratorComponent>();
            _generators = World.GetStash<ActivatedGenerator>();
            _timingComponentsStash = World.GetStash<TimingComponent>();
            _parabolaComponets = World.GetStash<ParabolaDropFromPlayerComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var berrySettings = ref _settings.BerriesSettings;
            foreach (var e in _filter)
            {
                if (e.Has<ActivatedGenerator>())
                {
                    ref var activatedComponent = ref _generators.Get(e);
                    ref var lastSpawnTime = ref _timingComponentsStash.Get(e).LastActionTime;

                    if (Time.time - lastSpawnTime >= berrySettings.BerryFlyDelay)
                    {
                        ShowFlyingBerries(e);
                        lastSpawnTime = Time.time;
                    }

                    if (_activatedBerries.Get(e).Berries.Count == 0)
                    {
                        e.RemoveComponent<GeneratorComponent>();
                        e.RemoveComponent<ActivatedGenerator>();
                    }
                }
            }
        }

        private void SetComponentSettings(Transform from, Transform to, Entity collectorEntity)
        {
            var ball = Object.Instantiate(_settings.ResBallFromPlayer);
            var entity = ball.GetComponent<ParabolaDropFromPlayerProvider>().Entity;

            entity.AddComponent<CollectableResourceComponent>().CollectorEntity = collectorEntity;
            _parabolaComponets.Get(entity).StartPosition = from;
            _parabolaComponets.Get(entity).EndPosition = to;
            ball.transform.position = from.position;
            
            var label = Object.Instantiate(_settings.FlyingLabelPrefab);
            label.transform.position = from.position;
        }
        
        private void ShowFlyingBerries(Entity entity)
        {
            var player = World.Filter.With<PlayerComponent>().Build().First();
            var playerPosition = player.GetComponent<TransformComponent>();
            
            ref var berrySettings = ref _settings.BerriesSettings;
            var berries = _activatedBerries.Get(entity).Berries;
            var berry = berries[0];
            berries.RemoveAt(0);

            SetComponentSettings(berry, playerPosition.Transform, player);
            
            var newEntity = World.CreateEntity();
            newEntity.AddComponent<TransformComponent>().Transform = berry;
            berry.gameObject.SetActive(false);
        }
    }
}