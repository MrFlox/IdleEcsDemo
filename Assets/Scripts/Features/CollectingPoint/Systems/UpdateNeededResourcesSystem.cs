using Features.CollectingPoint.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.CollectingPoint.Systems
{
    public class UpdateNeededResourcesSystem: UpdateSystem
    {

        private Filter _filter;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<BuildForResourcesComponent>().Build();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                e.GetComponent<BuildForResourcesComponent>().Text.text =
                    e.GetComponent<BuildForResourcesComponent>().ResourcesCount.ToString();
            }
        }
    }
}