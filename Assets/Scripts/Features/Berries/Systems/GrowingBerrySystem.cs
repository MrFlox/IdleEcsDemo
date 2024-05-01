using Features.Berries.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Features.Berries.Systems
{
    public class GrowingBerrySystem : SimpleSystem<GrowingBerryComponent>
    {
        private const float MaxScale = .4f;
        
        protected override void Process(Entity entity, ref GrowingBerryComponent component, in float deltaTime)
        {
            var target = new Vector3(MaxScale, MaxScale, MaxScale);
            if (component.Transform.localScale.x < MaxScale)
            {
                var newScale = Vector3.MoveTowards(component.Transform.localScale, target, .05f * deltaTime);
                component.Transform.localScale = newScale;
            }
            else
            {
                component.Finished = true;
            }
        }
    }
}