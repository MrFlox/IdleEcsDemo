using Features.Berries.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.Berries
{
    public sealed class BerriesFeature: UpdateFeature
    {
        private readonly IObjectResolver _container;

        public BerriesFeature(IObjectResolver container) => 
            _container = container;

        private T Resolve<T>() => _container.Resolve<T>();
        
        protected override void Initialize()
        {
            AddSystem(Resolve<SimpleFlyingBerrySystem>());
            AddSystem(Resolve<DeleteBerriesSystem>());
            AddSystem(Resolve<ActivateBerriesSystem>());
        }
    }
}