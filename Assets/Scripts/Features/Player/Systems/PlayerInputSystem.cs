using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Player.Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class PlayerInputSystem : UpdateSystem
    {
        private Filter _filter;
        private Entity _player;
        private Input _input;
        
        public override void OnAwake()
        {
            _input = new Input();
            _input.Default.Enable();
            
            _filter = World.Filter.With<Components.PlayerComponent>().With<TransformComponent>().Build();
            _player = _filter.First();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var player = ref _player.GetComponent<Components.PlayerComponent>();
            ref var transform = ref _player.GetComponent<TransformComponent>().Transform;
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