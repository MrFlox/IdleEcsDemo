using Features.Shared.Components;
using Scellecs.Morpeh;
using ScriptableObjects;

namespace Features.Shared.Systems
{
    public sealed class HilightObjectIfPlayerInRangeSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private Filter _meshRenders;
        private Stash<ActivateIfPlayerInRangeComp> _rangeComponents;

        private GameSettings _settings;

        public HilightObjectIfPlayerInRangeSystem(GameSettings settings) => _settings = settings;

        public override void OnAwake()
        {
            base.OnAwake();
            _meshRenders = World.Filter.With<ActivateIfPlayerInRangeComp>().With<TransformComponent>().Build();
            _rangeComponents = World.GetStash<ActivateIfPlayerInRangeComp>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _meshRenders)
                UpdateGenerator(entity);
        }

        private void UpdateGenerator(Entity entity)
        {
            SetGeneratorState(ref _rangeComponents.Get(entity), 
                ref entity.GetComponent<RadiusColliderComponent>(), entity);
        }
        
        private void SetGeneratorState(ref ActivateIfPlayerInRangeComp range, ref RadiusColliderComponent radius, Entity entity)
        {
            if (CheckDistanceWithPlayer(entity, radius.Radius))
                range.CircleMaterial.material.color = _settings.ActiveColor;
            else
                range.CircleMaterial.material.color = _settings.InactiveColor;
        }
    }
}