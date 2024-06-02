using System;
using System.Collections.Generic;
using Features.CollectingPoint.Components;
using Features.Generators.Providers;
using Features.Player.Components;
using Features.Shared.Components;
using Features.Shared.Providers;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.CollectingPoint.Systems
{
    public sealed class CollectingPointSystem : UpdateSystem
    {
        private const float ResourceSpawnDelay = .1f;

        private Filter _filter;
        private Stash<TransformComponent> _collectingPoints;
        private Stash<TimingComponent> _timingComponents;
        private Stash<ParabolaDropFromPlayerComponent> _parabolaComponets;
        private GameSettings _settings;
        private ResourceManager.Inventory _inventory;
        
        public CollectingPointSystem(GameSettings settings, ResourceManager.Inventory inventory)
        {
            _inventory = inventory;
            _settings = settings;
        }

        public override void OnAwake()
        {
            _filter = World.Filter.With<CollectingPointActivatedComponent>().Build();
            _collectingPoints = World.GetStash<TransformComponent>();
            _timingComponents = World.GetStash<TimingComponent>();
            _parabolaComponets = World.GetStash<ParabolaDropFromPlayerComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            var playerFilter = World.Filter.With<PlayerComponent>().Build();
            var player = playerFilter.First();
            var playerTransform = player.GetComponent<TransformComponent>().Transform;

            foreach (var e in _filter)
            {
                ref var lastActionTime = ref _timingComponents.Get(e).LastActionTime;

                if (Time.time - lastActionTime >= ResourceSpawnDelay)
                {
                    SpawnResourcesWithParabolaEffect(e, player, playerTransform);
                    lastActionTime = Time.time;
                }
            }
        }
        
        private void SpawnResourcesWithParabolaEffect(Entity e, Entity player, Transform playerTransform)
        {
            if (!e.Has<CollectingPointComponent>()) return;
            var direction = e.GetComponent<CollectingPointComponent>().Direction;

            if (direction == CollectingPointComponent.DirectionEnum.ToPlayer)
            {
                // if (e.Has<ResourcesStorageComponent>())
                // {
                //     ref var resourceCount = ref e.GetComponent<ResourcesStorageComponent>().Count;
                //     if (resourceCount > 0)
                //     {
                //         resourceCount--;
                //         SpawnResourceToPlayer(e, player, playerTransform, ResourceType.Green);
                //     }
                // }
                // else
                // {
                //     SpawnResourceToPlayer(e, player, playerTransform, ResourceType.Green);
                // }
            }
            else
            {
                if (player.Has<ResourcesStorageComponent>())
                {
                    SpawnToPlayerNewWay(e, player, playerTransform);
                }
                else
                {
                    SpawnResourceFromPlayer(e, playerTransform, ResourceType.Green);
                }
            }
        }
        private void SpawnToPlayerNewWay(Entity e, Entity player, Transform playerTransform)
        {
            ref var playerStorageComonent = ref player.GetComponent<ResourcesStorageComponent>();
            ref var neededResources = ref e.GetComponent<BuildForResourcesComponent>();
            ref var containsResources = ref e.GetComponent<ResourcesStorageComponent>();
            
            foreach (var resource in playerStorageComonent.Resources)
            {
                ref var resourceCount = ref resource.Amount;
                ref var spawnCounter = ref resource.SpawnCounter;

                if (resource.CurrentEntity != e)
                {
                    resource.CurrentEntity = e;
                    
                    var neededResource = neededResources.NeededResourcesList
                        .Find(x => x.Type == resource.Type);
                    var containsResource =  containsResources.Resources
                        .Find(x => x.Type == resource.Type);
                    var contains = 0;
                    if (neededResource != null )
                    {
                        contains = containsResource != null ? containsResource.Amount : 0;
                        var need = neededResource.Amount - contains;
                        spawnCounter = need;
                    }
                    else
                    {
                        continue;
                    }
                }
                
                

                if (resourceCount > 0 && spawnCounter > 0)
                {
                    spawnCounter--;
                    resourceCount--;
                    SpendResource(ref playerStorageComonent.Resources,
                        resource.Type, 1);
                    // _inventory.SpendResource(ResourceGeneratorComponent.ResourceType.Green, 1);
                    SpawnResourceFromPlayer(e, playerTransform,resource.Type);
                }
            }
        }
        private void SpendResource(ref List<ResourceAmount> resources, ResourceType type,
            int i)
        {
            var resourceStorageByType = resources.Find(x => x.Type == type);
            if (resourceStorageByType != null)
            {
                resourceStorageByType.Amount -= i;

                // if (resourceStorageByType.Amount == 0)
                // {
                //     resources.Remove(resourceStorageByType);
                // }
            }
            
        }

        private void SpawnResourceFromPlayer(Entity collectingEntity, Transform playerTransform,ResourceType type)
        {
            SetComponentSettings(playerTransform, _collectingPoints.Get(collectingEntity).Transform, collectingEntity,type);
        }

        private void SpawnResourceToPlayer(Entity e, Entity player, Transform playerTransform,ResourceType type)
        {
            SetComponentSettings(_collectingPoints.Get(e).Transform, playerTransform, player,type);
        }

        private void SetComponentSettings(Transform from, Transform to, Entity collectorEntity, ResourceType type)
        {
            var ball = Object.Instantiate(_settings.ResBallFromPlayer);
            ball.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = GetColorFromResourceType(type);
            var entity = ball.GetComponent<ParabolaDropFromPlayerProvider>().Entity;

            ref var c = ref entity.AddComponent<CollectableResourceComponent>();
            c.CollectorEntity = collectorEntity;
            c.Type = type;
            
            _parabolaComponets.Get(entity).StartPosition = from;
            _parabolaComponets.Get(entity).EndPosition = to;
            ball.transform.position = from.position;
        }
        
        private Color GetColorFromResourceType(ResourceType type)
        {
            return type switch
            {
                ResourceType.Green => Color.green,
                ResourceType.Red => Color.red,
                ResourceType.Yellow => Color.yellow,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}