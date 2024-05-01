using Features.Berries.Components;
using Features.Generators.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Berries.Systems
{
    public class BerriesGrowthSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _stash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GrowingBerriesComponent>().Build();
            _stash = World.GetStash<ResourceGeneratorComponent>();

            SetZeroScale();
        }
        private void SetZeroScale()
        {
            foreach (var entity in _filter)
            {
                ref var berries = ref _stash.Get(entity);
                foreach (var berry in berries._Berries)
                    berry.transform.localScale = Vector3.zero;
            }
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var berries = ref _stash.Get(entity);
                ref var index = ref berries.LastIndex;
                if (Time.time - berries.LastTime > .8f && index < berries._Berries.Count)
                {
                    AddBerryComponent(berries._Berries[index]);
                    index++;
                    berries.LastTime = Time.time;
                }
                if (index == berries._Berries.Count)
                {
                    entity.RemoveComponent<GrowingBerriesComponent>();
                }
            }
        }

        private void AddBerryComponent(Transform berry)
        {
            var newBerryEntity = World.CreateEntity();
            ref var component = ref newBerryEntity.AddComponent<GrowingBerryComponent>();
            component.Transform = berry;
            component.Transform.localScale = Vector3.zero;
        }
    }
}