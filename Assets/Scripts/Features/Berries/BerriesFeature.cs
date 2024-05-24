using Features.Berries.Systems;

namespace Features.Berries
{
    public sealed class BerriesFeature : UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<BerriesActivationSystem>());
            AddSystem(Resolve<SimpleFlyingBerrySystem>());
            AddSystem(Resolve<SpawnBerriesSystem>());
            AddSystem(Resolve<GrowingBerrySystem>());
            AddSystem(new RemoveGrowingFromGeneratorSystem());
        }
    }
}