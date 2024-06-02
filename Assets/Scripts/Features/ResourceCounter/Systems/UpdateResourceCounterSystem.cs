using Features.ResourceCounter.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.ResourceCounter.Systems
{
    public class UpdateResourceCounterSystem : UpdateSystem
    {
        private Filter _filter;

        public override void OnAwake()
        {
            _filter = World.Filter.With<ResourcesStorageComponent>().With<ResourceCounterUiComponent>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            // foreach (var e in _filter)
            // {
            //     e.GetComponent<ResourceCounterUiComponent>().Label.text =
            //         e.GetComponent<ResourcesStorageComponent>().Count.ToString();
            // }
        }
    }
}