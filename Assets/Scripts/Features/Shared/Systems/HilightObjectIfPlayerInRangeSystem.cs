using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using static Utils;

namespace Features.Shared.Systems
{
    public sealed class HilightObjectIfPlayerInRangeSystem : UpdateSystem
    {
        private Filter _meshRenders;
        private Filter _playersFilter;
        private Stash<TransformComponent> _generatorStash;
        private Stash<ActivateIfPlayerInRangeComp> _rangeComponents;
        private Entity _player;

        private GameSettings _settings;

        public HilightObjectIfPlayerInRangeSystem(GameSettings settings)
        {
            _settings = settings;
        }

        public override void OnAwake()
        {
            _meshRenders = World.Filter.With<ActivateIfPlayerInRangeComp>().With<TransformComponent>().Build();
            _playersFilter = World.Filter.With<PlayerComponent>().With<TransformComponent>().Build();
            _generatorStash = World.GetStash<TransformComponent>();
            _rangeComponents = World.GetStash<ActivateIfPlayerInRangeComp>();
            _player = _playersFilter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var playerTransform = ref _player.GetComponent<TransformComponent>();
            foreach (var entity in _meshRenders)
                UpdateGenerator(entity, playerTransform);
        }

        private void UpdateGenerator(Entity entity, TransformComponent playerTransform)
        {
            SetGeneratorState(
                playerTransform,  
                ref _generatorStash.Get(entity), 
                ref _rangeComponents.Get(entity), 
                ref entity.GetComponent<RadiusColliderComponent>());
        }
        
        private void SetGeneratorState(TransformComponent playerTransform, ref TransformComponent generator,
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