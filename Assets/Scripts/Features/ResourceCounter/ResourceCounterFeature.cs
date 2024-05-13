using Features.ResourceCounter.Systems;

namespace Features.ResourceCounter
{
    public class ResourceCounterFeature : UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<UpdateResourceCounterSystem>());
        }
    }
}