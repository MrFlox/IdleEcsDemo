using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(FlyingBerrySystem))]
    public sealed class FlyingBerrySystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<PositionOnStage> _moveStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<BerryComponent>().With<PositionOnStage>().Build();
            _moveStash = World.GetStash<PositionOnStage>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                ref var transform = ref _moveStash.Get(entity).Transform;
                var gameObject = transform.gameObject;
                var pos = transform.position;
                pos.y += .3f * deltaTime;

                transform.position = pos;
                if (pos.y > 4f)
                {
                    entity.RemoveComponent<BerryComponent>();
                    entity.RemoveComponent<PositionOnStage>();
                    Destroy(gameObject);
                }
            }
        }
    }
}