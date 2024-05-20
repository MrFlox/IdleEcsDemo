using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.Tiles
{
    public class HideTilesSystem : Initializer
    {
        private Filter _filter;
        private Stash<TransformComponent> _stash;

        public override void OnAwake()
        {
            _filter = World.Filter.With<GameTile>().With<TransformComponent>().Build();
            _stash = World.GetStash<TransformComponent>();

            foreach (var e in _filter)
            {
                _stash.Get(e).Transform.gameObject.SetActive(false);
                e.RemoveComponent<GameTile>();
            }
        }
    }
}