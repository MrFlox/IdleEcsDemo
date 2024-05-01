using Features.Berries.Systems;

namespace Features.Berries
{
    public sealed class BerriesFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(new BerriesGrowthSystem());
            AddSystem(Resolve<SimpleFlyingBerrySystem>());
            AddSystem(Resolve<DeleteBerriesSystem>());
            AddSystem(Resolve<ActivateBerriesSystem>());
            AddSystem(new GrowingBerrySystem());
            AddSystem(new RemoveGrowingFromGeneratorSystem());
        }
       
    }
}