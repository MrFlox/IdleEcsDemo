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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerInputSystem))]
    public sealed class PlayerInputSystem : UpdateSystem
    {
        private Filter _filter;
        private Entity _player;
        private Input _input;
        
        public override void OnAwake()
        {
            _input = new Input();
            _input.Default.Enable();
            
            _filter = World.Filter.With<Player>().With<PositionOnStage>().Build();
            _player = _filter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var player = ref _player.GetComponent<Player>();
            ref var transform = ref _player.GetComponent<PositionOnStage>().Transform;
            var moveInput = _input.Default.Move.ReadValue<Vector2>();
            player.Direction = new Vector3(moveInput.x, 0, moveInput.y);
            transform.Translate(player.Direction * 3 * Time.deltaTime);

            if (player.Direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(player.Direction, Vector3.up);
                player.Model.transform.rotation = Quaternion.RotateTowards(player.Model.transform.rotation, toRotation, Time.deltaTime * 300.0f);
            }
        }
    }
}