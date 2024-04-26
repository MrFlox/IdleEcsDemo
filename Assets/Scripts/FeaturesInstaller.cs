using System;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;
using Systems;
using VContainer;


class SimpleFeature: UpdateFeature
{
    private readonly TempClass _tempClass;

    public SimpleFeature(TempClass tempClass)
    {
        _tempClass = tempClass;
    }

    protected override void Initialize()
    {
        AddSystem(new AddGeneratorsSystem(_tempClass));
        // AddSystem(new FlyingBerrySystem());
        AddSystem(new SimpleFlyingBerrySystem());
        AddSystem(new GeneratorRadiusDrawerSystem());
        AddSystem(new PlayerInputSystem());
        AddSystem(new DeleteBerriesSystem());
        AddSystem(new PlayerAnimationSystem());
    }
}

public class FeaturesInstaller : BaseFeaturesInstaller
{

    [Inject] private TempClass _tempClass;
    
    protected override void InitializeShared()
    {
    }

    protected override UpdateFeature[] InitializeUpdateFeatures()
    {
        return new UpdateFeature[]
        {
            new SimpleFeature(_tempClass)
            // new PlayerInputFeature(),
            // new SpawnFeature(_cubePrefab),
            // new LoggerFeature()
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