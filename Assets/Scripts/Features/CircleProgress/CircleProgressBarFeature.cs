using Scellecs.Morpeh.Addons.Feature;

namespace Features.CircleProgress
{
    public class CircleProgressBarFeature: UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new ProgressBarUpdateSystem());
            // AddSystem(new RotateToCameraSystem());
        }
    }
}