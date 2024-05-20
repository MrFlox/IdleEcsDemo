﻿using Features.CollectingPoint.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;


namespace Features.CollectingPoint.Systems
{
    /// <summary>
    /// This system set ResourceCount to Needed - Count
    /// </summary>
    public class UpdateResourcesSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<BuildForResourcesComponent> _components;
        private Stash<ResourcesStorageComponent> _resourceComponents;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<BuildForResourcesComponent>().Build();
            _components = World.GetStash<BuildForResourcesComponent>();
            _resourceComponents = World.GetStash<ResourcesStorageComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                ref var buildForResourcesComp = ref _components.Get(e, out var isResourceCompExist);
                ref var resourceStorageComp = ref _resourceComponents.Get(e, out var isStorageCompExist);
                
                if(!isResourceCompExist || !isStorageCompExist) continue;
                
                buildForResourcesComp.ResourcesCount = buildForResourcesComp.NeededResources - resourceStorageComp.Count;
            
                if (buildForResourcesComp.ResourcesCount == 0)
                {
                    SpawnGenerator(e);
                    DestroyResourceCollector(e);
                }
            }
        }

        private void DestroyResourceCollector(Entity e)
        {
            Object.Destroy(e.GetComponent<TransformComponent>().Transform.gameObject);
            World.RemoveEntity(e);
        }
        
        private  void SpawnGenerator(Entity e)
        {
            // var generator = Object.Instantiate(_components.Get(e).Result);
            // generator.transform.position = e.GetComponent<TransformComponent>().Transform.position;
            _components.Get(e).Result.SetActive(true);
        }
    }
}