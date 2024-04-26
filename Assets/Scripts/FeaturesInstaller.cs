using System;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using VContainer;

public class FeaturesInstaller : BaseFeaturesInstaller
{

    [Inject] private SimpleFeature _simpleFeature;
    
    protected override void InitializeShared()
    {
    }

    protected override UpdateFeature[] InitializeUpdateFeatures()
    {
        return new UpdateFeature[]
        {
            _simpleFeature
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