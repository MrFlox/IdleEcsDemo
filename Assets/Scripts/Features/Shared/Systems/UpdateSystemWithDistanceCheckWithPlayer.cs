using Features.Player.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.Shared.Systems
{
    public abstract class UpdateSystemWithDistanceCheckWithPlayer : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _transforms;
        protected Entity Player;
        protected TransformComponent PlayerTransform;

        protected void GetTransformComponent(Entity e, out TransformComponent transform) => 
            transform = _transforms.Get(e);

        protected bool CheckDistanceWithPlayer(Entity e, float radius)
        {
            GetTransformComponent(e, out var first);
            return Utils.CheckDistance(ref first, ref PlayerTransform, radius);
        }

        public override void OnAwake()
        {
            _filter = World.Filter.With<PlayerComponent>().Build();
            _transforms = World.GetStash<TransformComponent>();
            Player = _filter.First();
            PlayerTransform = _transforms.Get(Player);
        }
    }
}