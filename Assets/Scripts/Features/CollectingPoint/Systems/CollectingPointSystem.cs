﻿using Features.CollectingPoint.Components;
using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.CollectingPoint.Systems
{
    public sealed class CollectingPointSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _collectingPoints;
        private Stash<TimingComponent> _timingComponents;

        public override void OnAwake()
        {
            _filter = World.Filter.With<CollectingPointActivatedComponent>().Build();
            _collectingPoints = World.GetStash<TransformComponent>();
            _timingComponents = World.GetStash<TimingComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();

            foreach (var e in _filter)
            {
                ref var lastActionTime = ref _timingComponents.Get(e).LastActionTime;

                if (Time.time - lastActionTime >= .5f)
                {
                    Debug.Log("Start Spawning Resources!!!");
                    lastActionTime = Time.time;
                }
            }
        }
    }
}