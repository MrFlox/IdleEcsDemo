using Generators.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Generators.Systems
{
    public class AddGeneratorsSystem: Initializer 
    {
        private readonly GameSettings _settings;
        private readonly TempClass _tempClass;
        private Filter _filter;
        private Stash<GeneratorsPositionsComponent> _moveStash;
        
        public AddGeneratorsSystem(GameSettings settings) => 
            _settings = settings;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GeneratorsPositionsComponent>().Build();
            _moveStash = World.GetStash<GeneratorsPositionsComponent>();

            var e = _filter.First();
            ref var g = ref _moveStash.Get(e);

            foreach (var pos in g.Positions)
            {
                Object.Instantiate(_settings.GeneratorPrefab, pos, Quaternion.identity);
            }
        }
    }
}