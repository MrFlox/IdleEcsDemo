using Features.Shared.Systems;

namespace Features.Shared
{
    public sealed class SharedFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<HilightObjectIfPlayerInRangeSystem>());
            AddSystem(Resolve<LootRotationSystem>());
            AddSystem(Resolve<ParabolaDropSystem>());
            AddSystem(Resolve<MoveToTransformSystem>());
            AddSystem(Resolve<ShadowUpdateSystem>());
            AddSystem(Resolve<ParabolaDropFromPlayer>());
        }
    }
}