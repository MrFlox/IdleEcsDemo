using Features.Berries.Systems;
using Features.Generators.Systems;
using Features.Player.Systems;
using Features.Shared.Systems;
using VContainer;

namespace DI
{
    internal class SystemRegistration
    {
        private readonly IContainerBuilder _builder;
        
        public SystemRegistration(IContainerBuilder builder) => _builder = builder;
        
        private void Register<T>() => _builder.Register<T>(Lifetime.Singleton);
        
        public void Register()
        {
            Register<ChopGeneratorSystem>();
            Register<ParabolaDropSystem>();
            Register<LootRotationSystem>();
            Register<ActivateBerriesSystem>();
            Register<BerriesGrowthSystem>();
            Register<GrowingBerrySystem>();
            Register<AddGeneratorsSystem>();
            Register<SimpleFlyingBerrySystem>();
            Register<HilightObjectIfPlayerInRangeSystem>();
            Register<PlayerInputSystem>();
            Register<DeleteBerriesSystem>();
            Register<PlayerAnimationSystem>();
        }
    }
}