using Features.Berries.Systems;

namespace Features.Berries
{
    public sealed class BerriesFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<SimpleFlyingBerrySystem>());
            AddSystem(Resolve<DeleteBerriesSystem>());
            AddSystem(Resolve<ActivateBerriesSystem>());
        }
       
    }
}