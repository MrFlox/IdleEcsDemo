using Features.CollectingPoint.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.CollectingPoint.Systems
{
    public class ResourceUIUpdater: UpdateSystem
    {
        private Filter _filter;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<BuildForResourcesComponent>().Build();

            foreach (var e in _filter)
            {
                ref var c = ref e.GetComponent<BuildForResourcesComponent>();
                if (c.Collector != null)
                {
                    c.Collector.UpdateValues(c.NeededResourcesList);
                }
            }
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                ref var c = ref e.GetComponent<BuildForResourcesComponent>();
                ref var c1 = ref e.GetComponent<ResourcesStorageComponent>();
                if (c.Collector != null)
                {
                    c.Collector.UpdateValuesTwo(c.NeededResourcesList, c1.Resources);
                }
            }
        }
    }
}