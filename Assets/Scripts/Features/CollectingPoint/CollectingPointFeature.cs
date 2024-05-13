using Features.CollectingPoint.Systems;

namespace Features.CollectingPoint
{
    public class CollectingPointFeature : UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<CollectingPointSystem>());
            AddSystem(Resolve<CollectingPointActivationSystem>());
            AddSystem(Resolve<CollectingResourcesSystem>());
        }
    }
}