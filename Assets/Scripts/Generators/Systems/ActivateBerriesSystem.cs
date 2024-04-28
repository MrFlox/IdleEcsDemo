using Berries.Providers;
using Components;
using Generators.Components;
using Generators.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Generators.Systems
{
    public class ActivateBerriesSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _activatedBerries;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<ActivatedGenerator>().Build();
            _activatedBerries = World.GetStash<ResourceGeneratorComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                if (entity.Has<ActivatedGenerator>())
                {
                    ShowFlyingBerries(entity);
                    entity.RemoveComponent<GeneratorComponent>();
                    entity.RemoveComponent<ActivatedGenerator>();
                }
            }
        }
        
        private void ShowFlyingBerries(Entity entity)
        {
            var berries = _activatedBerries.Get(entity)._Berries;
            foreach (var berry in berries)
            {
                var newEntity = World.CreateEntity();
                newEntity.AddComponent<PositionOnStage>().Transform = berry;
                ref var berryComponent = ref newEntity.AddComponent<BerryComponent>();
                berryComponent.Speed = Random.value + .2f;
                berryComponent.Entity = newEntity;
            }
        }
    }
}