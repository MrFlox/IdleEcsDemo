using Scellecs.Morpeh.Addons.Feature;
using VContainer;

public class UpdateFeatureWithContainer : UpdateFeature
{
    [Inject] private readonly IObjectResolver _container;

    protected T Resolve<T>() => _container.Resolve<T>();
    
    protected override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}