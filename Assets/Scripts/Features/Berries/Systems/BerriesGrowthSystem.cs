using Features.Berries.Components;
using Features.Generators.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;

namespace Features.Berries.Systems
{
    public class BerriesGrowthSystem : UpdateSystem
    {
        private readonly GameSettings _settings;

        public BerriesGrowthSystem(GameSettings settings) => _settings = settings;
     

        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _stash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GrowingBerriesComponent>().Build();
            _stash = World.GetStash<ResourceGeneratorComponent>();
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
            
            foreach (var entity in _filter)
            {
                ref var berries = ref _stash.Get(entity);
                ref var index = ref berries.LastIndex;
                if (Time.time - berries.LastTime > berriesSettings.BerryGrowthActivationDelay && index < berries.Berries.Count)
                {
                    AddBerryComponent(berries.Berries[index], index, entity);
                    index++;
                    berries.LastTime = Time.time;
                }
            }
        }

        private void AddBerryComponent(Transform berry, int index, Entity entity)
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