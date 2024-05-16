﻿using Features.Berries.Systems;
using Features.CollectingPoint.Systems;
using Features.Generators.Systems;
using Features.MoneyStack.Systems;
using Features.Player.Systems;
using Features.ResourceCounter.Systems;
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
            Register<MoveToTransformSystem>();
            Register<ShadowUpdateSystem>();
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
            Register<DeleteEntitiesSystem>();
            Register<PlayerAnimationSystem>();
            
            Register<CollectingPointSystem>();
            Register<CollectingPointActivationSystem>();
            Register<ParabolaDropFromPlayer>();
            Register<CollectingResourcesSystem>();
            
            Register<UpdateResourceCounterSystem>();
            Register<UpdateNeededResourcesSystem>();
            Register<UpdateResourcesSystem>();
            
            
            Register<MoneyStackSystem>();
            
        }
    }
}