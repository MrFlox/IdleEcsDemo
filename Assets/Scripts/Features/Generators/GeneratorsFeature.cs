using Features.Generators.Systems;

namespace Features.Generators
{
    public sealed class GeneratorsFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddInitializer(Resolve<AddGeneratorsSystem>());
            AddSystem(new GeneratrosActivatorSystem());
        }
    }
}