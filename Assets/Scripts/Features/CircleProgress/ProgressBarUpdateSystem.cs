using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Features.CircleProgress
{
    public class ProgressBarUpdateSystem : SimpleSystem<ProgressBarComponent>
    {
        protected override void Process(Entity entity, ref ProgressBarComponent component, in float deltaTime)
        {
            if (component.ProgressBarImage.fillAmount < 1)
                component.ProgressBarImage.fillAmount += .03f * deltaTime;
        }
    }
}