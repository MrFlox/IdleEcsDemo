using Features.FloatingObjects.Systems;

namespace Features.FloatingObjects
{
    public sealed class FloatingFeature: UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddInitializer(new FloatingInitializer());
            // AddSystem(new FloatingSystem());
            
            
        }
    }
}