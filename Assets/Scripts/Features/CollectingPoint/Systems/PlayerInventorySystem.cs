using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.CollectingPoint.Systems
{
    public class PlayerInventorySystem: UpdateSystem
    {
        private Filter _filter;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<ResourcesStorageComponent>().Build();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                if (e.GetComponent<ResourcesStorageComponent>().Ui != null)
                {
                    e.GetComponent<ResourcesStorageComponent>().Ui.UpdateValues(e.GetComponent<ResourcesStorageComponent>().Resources);
                }
            }
        }
    }
}