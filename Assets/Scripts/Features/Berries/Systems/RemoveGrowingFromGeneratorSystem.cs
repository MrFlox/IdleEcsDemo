using Features.Berries.Components;
using Features.Generators.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.Berries.Systems
{
    public class RemoveGrowingFromGeneratorSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _stash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GrowingBerryComponent>().Build();
            _stash = World.GetStash<ResourceGeneratorComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                ref var c = ref e.GetComponent<GrowingBerryComponent>();
                ref var generatorComponent = ref _stash.Get(c.Entity);
                
                if (c.Finished)
                {
                    if(c.Index == generatorComponent.Berries.Count - 1)
                        c.Entity.RemoveComponent<GrowingBerriesComponent>();
                    e.RemoveComponent<GrowingBerryComponent>();
                }
            }
        }
    }
}