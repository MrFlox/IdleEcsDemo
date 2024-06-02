using Features.CollectingPoint.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEditor.Localization.Plugins.XLIFF.V12;
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
                if(EnoughResources(e))
                {
                    ShowResult(e);
                    DestroyResourceCollector(e);
                }
            }
        }
        private bool EnoughResources(Entity entity)
        {
            var needed = _components.Get(entity);
            var contains = _resourceComponents.Get(entity);

            var result = true;
            foreach (var resources in needed.NeededResourcesList)
            {
                if (!isEqual(resources, contains))
                    result = false;
            }
            return result;
        }
        
        private bool isEqual(ResourceAmount resources, ResourcesStorageComponent contains)
        {
            var containsAmount = contains.Resources.Find(x => x.Type == resources.Type);
            if (containsAmount != null)
                return containsAmount.Amount == resources.Amount;
            return false;
        }

        private void DestroyResourceCollector(Entity e)
        {
            Object.Destroy(e.GetComponent<TransformComponent>().Transform.gameObject);
            World.RemoveEntity(e);
        }
        
        private  void ShowResult(Entity e)
        {
            _components.Get(e).Result.SetActive(true);
        }
    }
}