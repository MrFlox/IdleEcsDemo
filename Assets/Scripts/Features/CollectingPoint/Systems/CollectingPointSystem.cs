using Features.CollectingPoint.Components;
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
            var player = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build().First();
            ref var playerTransform = ref player.GetComponent<TransformComponent>();

            foreach (var e in _filter)
            {
                ref var lastActionTime = ref _timingComponents.Get(e).LastActionTime;

                if (Time.time - lastActionTime >= .1f)
                {
                    var ball = Object.Instantiate(_settings.ResBallFromPlayer);
                    var c = ball.GetComponent<ParabolaDropFromPlayerProvider>();
                    var en = c.Entity;
                    en.GetComponent<ParabolaDropFromPlayerComponent>().StartPosition =
                        _collectingPoints.Get(e).Transform;
                    ball.transform.position = _collectingPoints.Get(e).Transform.position;
                    lastActionTime = Time.time;
                }
            }
        }
    }
}