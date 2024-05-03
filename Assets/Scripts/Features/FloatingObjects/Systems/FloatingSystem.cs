using Features.FloatingObjects.Components;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;

namespace Features.FloatingObjects.Systems
{
    public sealed class FloatingSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<FloatingComponent> _moveStash;

        private const float Offset = .15f;

        public override void OnAwake()
        {
            _filter = World.Filter.With<FloatingComponent>().With<PositionOnStage>().Build();
            _moveStash = World.GetStash<FloatingComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
                UpdateObject(deltaTime, ref _moveStash.Get(entity), ref entity.GetComponent<PositionOnStage>());
        }

        private static void UpdateObject(float deltaTime, ref FloatingComponent movable,
            ref PositionOnStage positionOnStage)
        {
            ref var transform = ref positionOnStage.Transform;
            var newPos = transform.position;

            if (transform.position.y < movable.InitialY - Offset)
                movable.Direction = FloatingComponent.MoveDirection.Up;
            if (transform.position.y >= movable.InitialY + Offset)
                movable.Direction = FloatingComponent.MoveDirection.Down;


            if (movable.Direction == FloatingComponent.MoveDirection.Up)
                newPos.y += movable.MoveSpeed * deltaTime;
            else
                newPos.y -= movable.MoveSpeed * deltaTime;

            transform.position = newPos;
        }
    }
}