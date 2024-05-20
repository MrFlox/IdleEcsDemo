using Features.Generators.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using ScriptableObjects;
using UnityEngine;

namespace Features.Generators.Systems
{
    public class AddGeneratorsSystem : Initializer
    {
        private readonly GameSettings _settings;
        private Filter _filter;
        private Stash<GeneratorsPositionsComponent> _moveStash;

        public AddGeneratorsSystem(GameSettings settings) =>
            _settings = settings;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GeneratorsPositionsComponent>().Build();
            _moveStash = World.GetStash<GeneratorsPositionsComponent>();

            
            var e = _filter.First();
            ref var g = ref _moveStash.Get(e, out var isExist);

            foreach (var pos in g.Positions)
            {
                Object.Instantiate(_settings.GeneratorPrefab, pos.SetY(0.25f), Quaternion.identity);
            }
        }
    }
}