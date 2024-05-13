using Features.Berries.Components;
using Features.Generators.Components;
using Features.Generators.Providers;
using Features.Shared.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;

namespace Features.Generators.Systems
{
    public class GeneratorsActivatorSystem: UpdateSystemWithDistanceCheckWithPlayer
    {
        private Filter _generatorsFilter;

        public override void OnAwake()
        {
            base.OnAwake();
            _generatorsFilter = World.Filter.With<GeneratorComponent>().With<ResourceGeneratorComponent>().Without<GrowingBerriesComponent>().Build();
            World.GetStash<ResourceGeneratorComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _generatorsFilter)
                UpdateGenerator(entity);
        }

        private void UpdateGenerator(Entity entity)
        {
            SetGeneratorState(ref entity.GetComponent<RadiusColliderComponent>(), entity);
        }
        
        private void SetGeneratorState(ref RadiusColliderComponent radius, Entity entity)
        {
            if (CheckDistanceWithPlayer(entity, radius.Radius))
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