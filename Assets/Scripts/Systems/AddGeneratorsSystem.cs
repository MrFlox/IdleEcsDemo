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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(AddGeneratorsSystem))]
    public class AddGeneratorsSystem: Initializer 
    {
        private Filter _filter;
        private Stash<GeneratorsPositionsComponent> _moveStash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GeneratorsPositionsComponent>().Build();
            _moveStash = World.GetStash<GeneratorsPositionsComponent>();

            var e = _filter.First();
            ref var g = ref _moveStash.Get(e);

            foreach (var pos in g.Positions)
            {
                Instantiate(g.Prefab, pos, Quaternion.identity);
            }
        }
    }
}