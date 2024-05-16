using DG.Tweening;
using Features.MoneyStack.Components;
using Features.Shared.Components;
using Features.Shared.Systems;
using Scellecs.Morpeh;
using UnityEngine;

namespace Features.MoneyStack.Systems
{
    public class MoneySplashSystem : UpdateSystemWithDistanceCheckWithPlayer
    {
        private Filter _filter;
        private Stash<TimingComponent> _timingStash;
        private Stash<Components.MoneyStack> _moneyStash;
        private Stash<TransformComponent> _transformStash;
        private Stash<ActivatedMoneyStack> _activatedMoneyStash;

        public override void OnAwake()
        {
            base.OnAwake();
            _filter = World.Filter.With<ActivatedMoneyStack>().Build();
            _timingStash = World.GetStash<TimingComponent>();
            _moneyStash = World.GetStash<Components.MoneyStack>();
            _transformStash = World.GetStash<TransformComponent>();
            _activatedMoneyStash = World.GetStash<ActivatedMoneyStack>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                if (CheckDistanceWithPlayer(e, 1f))
                    SplashAllMoneyStack(e);
            }
        }
        private void SplashAllMoneyStack(Entity entity)
        {
            ref var list = ref _moneyStash.Get(entity).Money;

            foreach (var money in list)
            {
                var lastY = money.transform.position.y;
                money.transform.DOMoveY(lastY + 1f, .5f);
                DOVirtual.DelayedCall(.5f, () => Object.Destroy(money));
            }

            entity.RemoveComponent<ActivatedMoneyStack>();
        }
    }
}