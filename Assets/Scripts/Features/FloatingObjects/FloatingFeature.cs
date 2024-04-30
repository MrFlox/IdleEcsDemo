using Features.FloatingObjects.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.FloatingObjects
{
    public sealed class FloatingFeature: UpdateFeature
    {
        private readonly IObjectResolver _container;

        public FloatingFeature(IObjectResolver container) => 
            _container = container;
        
        private T Resolve<T>() => _container.Resolve<T>();
        
        protected override void Initialize()
        {
            AddInitializer(new FloatingInitializer());
            AddSystem(new FloatingSystem());
        }
    }
}