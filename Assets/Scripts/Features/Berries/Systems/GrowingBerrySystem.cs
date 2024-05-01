using Features.Berries.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Features.Berries.Systems
{
    public class GrowingBerrySystem:SimpleSystem<GrowingBerryComponent>
    {
        protected override void Process(Entity entity, ref GrowingBerryComponent component, in float deltaTime)
        {
            const float MaxScale = .4f;
            var target = new Vector3(MaxScale, MaxScale, MaxScale);
            if (component.Transform.localScale.x < 1)
            {
                var newScale = Vector3.MoveTowards(component.Transform.localScale, target, .05f * deltaTime);
                component.Transform.localScale = newScale;
            }
        }
    }
}