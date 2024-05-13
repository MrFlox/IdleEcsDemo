using System;
using Features.Shared.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;
using static Utils;

namespace Features.CollectingPoint.Systems
{
    public class CollectingResourcesSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private const float CollectResourceRadius = .2f;

        private Filter _filter;
        private Stash<CollectableResourceComponent> _stash;
        private Stash<TransformComponent> _transformStash;
        public override void OnAwake()
        {
            base.OnAwake();
            _filter = World.Filter.With<CollectableResourceComponent>().Build();
            _stash = World.GetStash<CollectableResourceComponent>();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                var collectorEntity = _stash.Get(e).CollectorEntity;
                if (collectorEntity == null) throw new Exception("No Collector Target");
                
                ref var resourceTransform = ref e.GetComponent<TransformComponent>();
                ref var collectorTransform = ref _transformStash.Get(collectorEntity);

                if (CheckDistance(ref resourceTransform, ref collectorTransform, CollectResourceRadius))
                {
                    if (collectorEntity.Has<ResourcesStorageComponent>())
                    {
                        ref var count = ref collectorEntity.GetComponent<ResourcesStorageComponent>().Count;
                        count++;
                    }
                    e.AddComponent<DeleteComponent>();
                    e.RemoveComponent<CollectableResourceComponent>();
                }
            }
        }
    }
}