﻿using Features.CollectingPoint.Systems;
using Features.Tiles;

namespace Features.CollectingPoint
{
    public class CollectingPointFeature : UpdateFeatureWithContainer
    {
        protected override void Initialize()
        {
            AddSystem(Resolve<CollectingPointSystem>());
            AddSystem(Resolve<CollectingPointActivationSystem>());
            AddSystem(Resolve<CollectingResourcesSystem>());
            AddSystem(Resolve<UpdateNeededResourcesSystem>());
            AddSystem(Resolve<UpdateResourcesSystem>());
            AddInitializer(new HideTilesSystem());
        }
    }
}