using Features.Berries.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.Berries.Systems
{
    public class BerriesGrowthSystem:UpdateSystem
    {
        private Filter _filter;
        private Stash<BerryComponent> _stash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<BerryComponent>().Build();
            _stash = World.GetStash<BerryComponent>();


            // foreach (var VARIABLE in COLLECTION)
            // {
            //     
            // }
            
        }
        
        public override void OnUpdate(float deltaTime)
        {
        }
    }
}