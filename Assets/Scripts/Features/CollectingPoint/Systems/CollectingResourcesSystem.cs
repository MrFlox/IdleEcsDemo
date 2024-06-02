using System;
using Features.CollectingPoint.Components;
using Features.Generators.Providers;
using Features.Player.Components;
using Features.Shared.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using VContainer;
using static Features.Generators.Providers.ResourceGeneratorComponent;
using static Utils;

namespace Features.CollectingPoint.Systems
{
    public class CollectingResourcesSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private const float CollectResourceRadius = .2f;

        private Filter _filter;
        private Stash<CollectableResourceComponent> _stash;
        private Stash<TransformComponent> _transformStash;
        [Inject] private ResourceManager.Inventory _inventory;
        
        
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
                if(e.Has<DeleteComponent>()) continue;
                var collectorEntity = _stash.Get(e).CollectorEntity;
                var type = _stash.Get(e).Type;
                if (collectorEntity == null) throw new Exception("No Collector Target");
                if(collectorEntity.IsNullOrDisposed()) continue;                
                ref var resourceTransform = ref e.GetComponent<TransformComponent>();
                ref var collectorTransform = ref _transformStash.Get(collectorEntity);

                if (CheckDistance(ref resourceTransform, ref collectorTransform, CollectResourceRadius))
                {
                    if (collectorEntity.Has<ResourcesStorageComponent>())
                    {
                        AddResource(type, 1, ref collectorEntity.GetComponent<ResourcesStorageComponent>());
                        if(collectorEntity.Has<PlayerComponent>())
                            _inventory.AddResource(type, 1);
                    }
                    e.AddComponent<DeleteComponent>();
                    e.RemoveComponent<CollectableResourceComponent>();
                }
            }
        }
        private void AddResource(ResourceType type, int i, ref ResourcesStorageComponent component)
        {
            if (component.Resources == null)
            {
                component.Resources = new();
            }

            bool added = false;
            foreach (var resource in component.Resources)
            {
                if (resource.Type == type)
                {
                    resource.Amount++;
                    added = true;
                }
            }
            if (!added)
            {
                component.Resources.Add(new ResourceAmount { Amount = 1, Type = type});
            }
                
        }
    }
}