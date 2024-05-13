﻿using Features.CollectingPoint.Components;
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
        private Filter _filter;
        private Stash<TransformComponent> _collectingPoints;
        private Stash<TimingComponent> _timingComponents;
        private GameSettings _settings;

        public CollectingPointSystem(GameSettings settings) => _settings = settings;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<CollectingPointActivatedComponent>().Build();
            _collectingPoints = World.GetStash<TransformComponent>();
            _timingComponents = World.GetStash<TimingComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            var playerFilter = World.Filter.With<PlayerComponent>().Build();
            var player = playerFilter.First();
            var playerTransform = player.GetComponent<TransformComponent>().Transform;
            
            foreach (var e in _filter)
            {
                ref var lastActionTime = ref _timingComponents.Get(e).LastActionTime;
            
                if (Time.time - lastActionTime >= .1f)
                {
                    var direction = e.GetComponent<CollectingPointComponent>().Direction;
                    if (direction == CollectingPointComponent.DirectionEnum.ToPlayer)
                    {
                        var ball = Object.Instantiate(_settings.ResBallFromPlayer);
                        var resourceEntity = ball.GetComponent<ParabolaDropFromPlayerProvider>().Entity;
                        resourceEntity.AddComponent<CollectableResourceComponent>().CollectorEntity = player;
                        resourceEntity.GetComponent<ParabolaDropFromPlayerComponent>().StartPosition =
                            _collectingPoints.Get(e).Transform;
                        resourceEntity.GetComponent<ParabolaDropFromPlayerComponent>().EndPosition =
                            playerTransform;
                        ball.transform.position = _collectingPoints.Get(e).Transform.position;
                        
                    }
                    else
                    {
                        var ball = Object.Instantiate(_settings.ResBallFromPlayer);
                        var resourceEntity = ball.GetComponent<ParabolaDropFromPlayerProvider>().Entity;
                        resourceEntity.AddComponent<CollectableResourceComponent>().CollectorEntity = e;
                        resourceEntity.GetComponent<ParabolaDropFromPlayerComponent>().StartPosition =
                            playerTransform;
                        resourceEntity.GetComponent<ParabolaDropFromPlayerComponent>().EndPosition =
                            _collectingPoints.Get(e).Transform;
                        ball.transform.position = playerTransform.position;
                    }
                    
                    lastActionTime = Time.time;
                }
            }
        }
    }
}