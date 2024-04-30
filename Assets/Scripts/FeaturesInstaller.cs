using System;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using Scellecs.Morpeh.Addons.Unity.VContainer;
using VContainer;

public class FeaturesInstaller : BaseFeaturesInstaller
{

    [Inject] private IObjectResolver _container;
    
    protected override void InitializeShared()
    {
    }

    protected override UpdateFeature[] InitializeUpdateFeatures()
    {
        return new UpdateFeature[]
        {
            _container.CreateFeature<SimpleFeature>()
        };
    }

    protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures()
    {
        return Array.Empty<FixedUpdateFeature>();
    }

    protected override LateUpdateFeature[] InitializeLateUpdateFeatures()
    {
        return Array.Empty<LateUpdateFeature>();
    }
}