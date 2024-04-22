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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(PlayerAnimationSystem))]
    public sealed class PlayerAnimationSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<Player> _moveStash;
        private Stash<PlayerAnimator> _animatorStash;
        private string _stateName;
        private string _idle;

        public override void OnAwake()
        {
            _filter = World.Filter.With<Player>().Build();
            _moveStash = World.GetStash<Player>();
            _animatorStash = World.GetStash<PlayerAnimator>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var player = ref _moveStash.Get(_filter.First());
            ref var animator = ref _animatorStash.Get(_filter.First()).Animator;

            if (player.Direction != Vector3.zero)
            {
                if (_stateName != "run")
                    _stateName = "run";
            }
            else
            {
                if (_stateName != "idle")
                    _stateName = "idle";
            }
            animator.Play(_stateName);
        }
    }
}