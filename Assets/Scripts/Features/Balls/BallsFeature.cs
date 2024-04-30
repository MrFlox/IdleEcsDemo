using Features.Balls.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Features.Balls
{
    public sealed class BallsFeature: UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new BallGeneratorSystem());
            AddSystem(new SpawningBallsSystem());
        }
    }
}