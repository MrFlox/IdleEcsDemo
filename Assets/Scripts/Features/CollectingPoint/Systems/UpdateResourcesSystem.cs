using Features.CollectingPoint.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.CollectingPoint.Systems
{
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
                ref var buildForResourcesComp = ref _components.Get(e);
                ref var resourceStorageComp = ref _resourceComponents.Get(e);
                buildForResourcesComp.ResourcesCount = buildForResourcesComp.NeededResources - resourceStorageComp.Count;

                if (buildForResourcesComp.ResourcesCount < 0)
                {
                    SpawnGenerator(e);
                    e.RemoveComponent<BuildForResourcesComponent>();
                    e.RemoveComponent<ResourcesStorageComponent>();
                    
                    // e.GetComponent<TransformComponent>().Transform.gameObject.SetActive(false);
                    // e.AddComponent<DeleteComponent>();
                }
            }
        }
        
        private  void SpawnGenerator(Entity e)
        {
            var generator = Object.Instantiate(_components.Get(e).Result);
            generator.transform.position = e.GetComponent<TransformComponent>().Transform.position;
        }
    }
}