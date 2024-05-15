using Features.Player.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Player.Systems
{
    public sealed class PlayerAnimationSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<Components.PlayerComponent> _moveStash;
        private Stash<PlayerAnimator> _animatorStash;
        private string _stateName;
        private string _idle;
        private readonly static int Run = Animator.StringToHash("run");

        public override void OnAwake()
        {
            _filter = World.Filter.With<Components.PlayerComponent>().Build();
            _moveStash = World.GetStash<Components.PlayerComponent>();
            _animatorStash = World.GetStash<PlayerAnimator>();
        }

        public override void OnUpdate(float deltaTime)
        {
            ref var player = ref _moveStash.Get(_filter.First());
            ref var animatorComponent = ref _animatorStash.Get(_filter.First());
            
            if (player.Direction != Vector3.zero)
            {
                SetState("run", ref animatorComponent);
            }
            else
            {
                SetState("idle", ref animatorComponent);
            }
            
        }
        private void SetState(string newState, ref PlayerAnimator animator)
        {
            if(animator.AnimationState == newState) return;
            animator.AnimationState = newState;
            
            switch (newState)
            {
                case "run":
                    Debug.Log("Run");
                    animator.Animator.Play("run");
                    break;
                
                case "idle":
                    Debug.Log("idle");
                    // animator.Animator.Play("pickup");
                    animator.Animator.Play("idle");
                    break;
            }
        }
    }
}