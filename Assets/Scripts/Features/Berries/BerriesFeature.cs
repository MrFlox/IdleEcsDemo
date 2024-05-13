using Features.Berries.Systems;

namespace Features.Berries
{
    public sealed class BerriesFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<BerriesGrowthSystem>());
            AddSystem(Resolve<SimpleFlyingBerrySystem>());
            AddSystem(Resolve<ActivateBerriesSystem>());
            AddSystem(Resolve<GrowingBerrySystem>());
            AddSystem(new RemoveGrowingFromGeneratorSystem());
        }
    }
}