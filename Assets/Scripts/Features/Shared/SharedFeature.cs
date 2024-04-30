using Features.Shared.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.Shared
{
    public sealed class SharedFeature: UpdateFeature
    {
        private IObjectResolver _container;

        public SharedFeature(IObjectResolver container) => 
            _container = container;

        private T Resolve<T>() => _container.Resolve<T>();
        
        protected override void Initialize()
        {
            AddSystem(Resolve<HilightObjectIfPlayerInRangeSystem>());
        }
    }
}