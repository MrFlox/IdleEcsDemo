using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Features.CircleProgress
{
    public class RotateToCameraSystem : SimpleSystem<ProgressBarComponent>
    {
        protected override void Process(Entity entity, ref ProgressBarComponent component, in float deltaTime)
        {
            var canvas = component.Canvas;
            var camera = component.Camera;
            var transform = canvas.transform;

            transform.LookAt(camera.transform.position);
        }
    }
}