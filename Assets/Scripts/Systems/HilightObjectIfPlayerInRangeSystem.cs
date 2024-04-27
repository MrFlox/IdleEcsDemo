using Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using Scellecs.Morpeh.Addons.Systems;
using Systems.Helpers;

namespace Systems
{

    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class HilightObjectIfPlayerInRangeSystem : UpdateSystem
    {
        private Filter _meshRenders;
        private Filter _playersFilter;
        private Stash<PositionOnStage> _generatorStash;
        private Stash<ActivateIfPlayerInRangeComp> _rangeComponents;
        private Entity _player;

        public override void OnAwake()
        {
            _meshRenders = World.Filter.With<ActivateIfPlayerInRangeComp>().With<PositionOnStage>().Build();
            _playersFilter = World.Filter.With<Player>().With<PositionOnStage>().Build();
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
            SetGeneratorState(playerTransform,  ref _generatorStash.Get(entity), ref _rangeComponents.Get(entity));
        }

        private void SetGeneratorState(PositionOnStage playerTransform, ref PositionOnStage generator, ref ActivateIfPlayerInRangeComp range)
        {
            if (Vector2.Distance(playerTransform.Transform.position.XZVector(), generator.Transform.position.XZVector()) < range.Radius)
            {
                range.CircleMaterial.material.color = Color.green;
            }
            else
                range.CircleMaterial.material.color = Color.red;
        }
    }
}