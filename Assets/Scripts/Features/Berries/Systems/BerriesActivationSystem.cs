using Features.Berries.Components;
using Features.Generators.Providers;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;

namespace Features.Berries.Systems
{
    /// <summary>
    /// Система, которая реализует рост ягод на кусте
    /// </summary>
    public class BerriesActivationSystem : UpdateSystem
    {
        private readonly GameSettings _settings;

        public BerriesActivationSystem(GameSettings settings) => _settings = settings;

        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _stash;
        private Stash<TimingComponent> _timingComponentsStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GrowingBushComponent>().Build();
            _stash = World.GetStash<ResourceGeneratorComponent>();
            _timingComponentsStash = World.GetStash<TimingComponent>();
        }
        
        private void SetZeroScale()
        {
            foreach (var entity in _filter)
            {
                ref var berries = ref _stash.Get(entity);
                if(berries.Inited) continue;
                foreach (var berry in berries.Berries)
                {
                    berry.transform.localScale = Vector3.zero;
                    berries.Inited = true;
                }
            }
        }
        
        public override void OnUpdate(float deltaTime)
        {
            ref var berriesSettings = ref _settings.BerriesSettings;
            
            SetZeroScale();
            
            foreach (var e in _filter)
            {
                ref var berries = ref _stash.Get(e);
                ref var index = ref berries.LastIndex;
                ref var lastTime = ref _timingComponentsStash.Get(e).LastActionTime;
                if (Time.time - lastTime > berriesSettings.BerryGrowthActivationDelay && index < berries.Berries.Count)
                {
                    CreateBerryComponent(berries.Berries[index], index, e);
                    index++;
                    lastTime = Time.time;
                }
            }
        }

        private void CreateBerryComponent(Transform berry, int index, Entity entity)
        {
            var newBerryEntity = World.CreateEntity();
            ref var c = ref newBerryEntity.AddComponent<GrowingBerryComponent>();
            
            c.Index = index;
            c.Transform = berry;
            c.Transform.localScale = Vector3.zero;
            c.Entity = entity;
        }
    }
}