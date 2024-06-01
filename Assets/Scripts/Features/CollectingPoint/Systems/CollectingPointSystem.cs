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
                if (e.Has<ResourcesStorageComponent>())
                {
                    ref var resourceCount = ref e.GetComponent<ResourcesStorageComponent>().Count;
                    if (resourceCount > 0)
                    {
                        resourceCount--;
                        SpawnResourceToPlayer(e, player, playerTransform);
                    }
                }
                else
                {
                    SpawnResourceToPlayer(e, player, playerTransform);
                }
            }
            else
            {
                if (player.Has<ResourcesStorageComponent>())
                {
                    SpawnToPlayerNewWay(e, player, playerTransform);
                }
                else
                {
                    SpawnResourceFromPlayer(e, playerTransform);
                }
            }
        }
        private void SpawnToPlayerNewWay(Entity e, Entity player, Transform playerTransform)
        {
            ref var playerStorageComonent = ref player.GetComponent<ResourcesStorageComponent>();
            foreach (var resource in playerStorageComonent.Resources)
            {
                ref var resourceCount = ref resource.Amount;
                ref var spawnCounter = ref resource.SpawnCounter;

                if (playerStorageComonent.CurrentEntity != e)
                {
                    playerStorageComonent.CurrentEntity = e;
                }
                var neededResource = e.GetComponent<BuildForResourcesComponent>().NeededResourcesList
                    .Find(x => x.Type == resource.Type);
                if (neededResource != null)
                {
                    spawnCounter = neededResource.Amount;
                }
                else
                {
                    continue;
                }

                if (resourceCount > 0 && spawnCounter > 0)
                {
                    spawnCounter--;
                    resourceCount--;
                    SpendResource(ref playerStorageComonent.Resources,
                        ResourceGeneratorComponent.ResourceType.Green, 1);
                    _inventory.SpendResource(ResourceGeneratorComponent.ResourceType.Green, 1);
                    SpawnResourceFromPlayer(e, playerTransform);
                }
            }
        }
        private void SpendResource(ref List<ResourceAmount> resources, ResourceGeneratorComponent.ResourceType type,
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

        private void SpawnResourceFromPlayer(Entity collectingEntity, Transform playerTransform)
        {
            SetComponentSettings(playerTransform, _collectingPoints.Get(collectingEntity).Transform, collectingEntity);
        }

        private void SpawnResourceToPlayer(Entity e, Entity player, Transform playerTransform)
        {
            SetComponentSettings(_collectingPoints.Get(e).Transform, playerTransform, player);
        }

        private void SetComponentSettings(Transform from, Transform to, Entity collectorEntity)
        {
            var ball = Object.Instantiate(_settings.ResBallFromPlayer);
            var entity = ball.GetComponent<ParabolaDropFromPlayerProvider>().Entity;

            entity.AddComponent<CollectableResourceComponent>().CollectorEntity = collectorEntity;
            _parabolaComponets.Get(entity).StartPosition = from;
            _parabolaComponets.Get(entity).EndPosition = to;
            ball.transform.position = from.position;
        }
    }
}