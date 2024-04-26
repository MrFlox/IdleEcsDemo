using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;


namespace Systems
{
    public class AddGeneratorsSystem: UpdateSystem 
    {
        private readonly TempClass _tempClass;
        private Filter _filter;
        private Stash<GeneratorsPositionsComponent> _moveStash;

        public AddGeneratorsSystem(TempClass tempClass)
        {
            _tempClass = tempClass;
        }

        public override void OnAwake()
        {
            _tempClass.HelloWorld();
            _filter = World.Filter.With<GeneratorsPositionsComponent>().Build();
            _moveStash = World.GetStash<GeneratorsPositionsComponent>();

            var e = _filter.First();
            ref var g = ref _moveStash.Get(e);

            foreach (var pos in g.Positions)
            {
                Object.Instantiate(g.Prefab, pos, Quaternion.identity);
            }
        }
        public override void OnUpdate(float deltaTime)
        {
        }
    }
}