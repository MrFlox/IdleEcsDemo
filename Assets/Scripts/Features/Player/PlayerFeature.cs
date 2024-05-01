using Features.Player.Systems;

namespace Features.Player
{
    public sealed class PlayerFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<PlayerInputSystem>());
            AddSystem(Resolve<PlayerAnimationSystem>());
        }
    }
}