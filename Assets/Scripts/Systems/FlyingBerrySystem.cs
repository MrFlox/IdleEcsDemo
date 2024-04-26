using Components;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UpdateSystem = Scellecs.Morpeh.Addons.Systems.UpdateSystem;


namespace Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
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
                if(!entity.Has<BerryComponent>()) continue;
                
                ref var transform = ref _moveStash.Get(entity).Transform;
                ref var berry = ref entity.GetComponent<BerryComponent>();
                var pos = transform.position;
                pos.y += berry.Speed * deltaTime;

                transform.position = pos;
                if (pos.y > 4f)
                {
                    if (!entity.Has<DeleteComponent>())
                        entity.AddComponent<DeleteComponent>();
                }
            }
        }
    }
}