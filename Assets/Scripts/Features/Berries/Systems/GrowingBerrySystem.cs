using Features.Berries.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using ScriptableObjects;
using UnityEngine;

namespace Features.Berries.Systems
{
    /// <summary>
    /// Система, отвечающая за рост каждой отдельной ягоды на кусту
    /// </summary>
    public class GrowingBerrySystem : SimpleSystem<GrowingBerryComponent>
    {
        private readonly GameSettings _settings;
        
        public GrowingBerrySystem(GameSettings settings) => _settings = settings;

        protected override void Process(Entity entity, ref GrowingBerryComponent component, in float deltaTime)
        {
            var target = new Vector3(_settings.BerriesSettings.BerryMaxScale, _settings.BerriesSettings.BerryMaxScale, _settings.BerriesSettings.BerryMaxScale);
            if (component.Transform.localScale.x < _settings.BerriesSettings.BerryMaxScale)
            {
                var newScale = Vector3.MoveTowards(component.Transform.localScale, target, _settings.BerriesSettings.BerryGrowthSpeed * deltaTime);
                component.Transform.localScale = newScale;
            }
            else
            {
                component.Finished = true;
            }
        }
    }
}