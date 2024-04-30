using Features.Generators.Systems;
using Scellecs.Morpeh.Addons.Feature;
using VContainer;

namespace Features.Generators
{
    public sealed class GeneratorsFeature: UpdateFeature
    {
        private readonly IObjectResolver _container;

        public GeneratorsFeature(IObjectResolver container) => 
            _container = container;

        private T Resolve<T>() => _container.Resolve<T>();

        protected override void Initialize()
        {
            AddInitializer(Resolve<AddGeneratorsSystem>());
            AddSystem(new GeneratrosActivatorSystem());
        }
    }
}