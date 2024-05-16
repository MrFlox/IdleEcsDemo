using System;
using Features.Balls;
using Features.Berries;
using Features.CircleProgress;
using Features.CollectingPoint;
using Features.FloatingObjects;
using Features.Generators;
using Features.MoneyStack;
using Features.Player;
using Features.ResourceCounter;
using Features.Shared;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using Scellecs.Morpeh.Addons.Unity.VContainer;
using VContainer;

public class FeaturesInstaller : BaseFeaturesInstaller
{

    [Inject] private IObjectResolver _container;
    
    protected override void InitializeShared() { }

    protected override UpdateFeature[] InitializeUpdateFeatures()
    {
        return new UpdateFeature[]
        {
            _container.CreateFeature<FloatingFeature>(),
            _container.CreateFeature<BerriesFeature>(),
            _container.CreateFeature<BallsFeature>(),
            _container.CreateFeature<SharedFeature>(),
            _container.CreateFeature<PlayerFeature>(),
            _container.CreateFeature<CircleProgressBarFeature>(),
            _container.CreateFeature<GeneratorsFeature>(),
            _container.CreateFeature<CollectingPointFeature>(),
            _container.CreateFeature<ResourceCounterFeature>(),
            _container.CreateFeature<MoneyStackFeature>(),
        };
    }

    protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures() => Array.Empty<FixedUpdateFeature>();

    protected override LateUpdateFeature[] InitializeLateUpdateFeatures() => Array.Empty<LateUpdateFeature>();
}