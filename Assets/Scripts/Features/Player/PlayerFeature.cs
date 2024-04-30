using Features.Player.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.Player
{
    public sealed class PlayerFeature: UpdateFeature
    {
        private readonly IObjectResolver _container;

        public PlayerFeature(IObjectResolver container) => 
            _container = container;

        private T Resolve<T>() => _container.Resolve<T>();
        
        protected override void Initialize()
        {
            AddSystem(Resolve<PlayerInputSystem>());
            AddSystem(Resolve<PlayerAnimationSystem>());
        }
    }
}