using Components;
using Player.Components;
using Scellecs.Morpeh;
using UnityEngine;
using Scellecs.Morpeh.Addons.Systems;
using Systems.Helpers;
using VContainer;
using static Systems.Utils;

namespace Systems
{
    public sealed class HilightObjectIfPlayerInRangeSystem : UpdateSystem
    {
        private Filter _meshRenders;
        private Filter _playersFilter;
        private Stash<PositionOnStage> _generatorStash;
        private Stash<ActivateIfPlayerInRangeComp> _rangeComponents;
        private Entity _player;

        private GameSettings _settings;

        public HilightObjectIfPlayerInRangeSystem(GameSettings settings)
        {
            _settings = settings;
        }

        public override void OnAwake()
        {
            _meshRenders = World.Filter.With<ActivateIfPlayerInRangeComp>().With<PositionOnStage>().Build();
            _playersFilter = World.Filter.With<PlayerComponent>().With<PositionOnStage>().Build();
            _generatorStash = World.GetStash<PositionOnStage>();
            _rangeComponents = World.GetStash<ActivateIfPlayerInRangeComp>();
            _player = _playersFilter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerTransform = ref _player.GetComponent<PositionOnStage>();
            foreach (var entity in _meshRenders)
                UpdateGenerator(entity, playerTransform);
        }

        private void UpdateGenerator(Entity entity, PositionOnStage playerTransform)
        {
            SetGeneratorState(
                playerTransform,  
                ref _generatorStash.Get(entity), 
                ref _rangeComponents.Get(entity), 
                ref entity.GetComponent<RadiusColliderComponent>());
        }
        
        private void SetGeneratorState(PositionOnStage playerTransform, ref PositionOnStage generator,
            ref ActivateIfPlayerInRangeComp range, ref RadiusColliderComponent radius)
        {
            if (CheckDistance(ref playerTransform, ref generator, radius.Radius))
            {
                range.CircleMaterial.material.color = _settings.ActiveColor;
            }
            else
                range.CircleMaterial.material.color = _settings.InactiveColor;
        }
    }
}