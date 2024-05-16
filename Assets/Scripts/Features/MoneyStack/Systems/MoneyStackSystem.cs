using DG.Tweening;
using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.MoneyStack.Systems
{
    public class MoneyStackSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TimingComponent> _timingStash;
        private Stash<Components.MoneyStack> _moneyStash;
        private Stash<TransformComponent> _transformStash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<Components.MoneyStack>().Build();
            _timingStash = World.GetStash<TimingComponent>();
            _moneyStash = World.GetStash<Components.MoneyStack>();
            _transformStash = World.GetStash<TransformComponent>();
        }

        private void AddStackOfMoney(Entity e)
        {
            ref var moneyComp = ref _moneyStash.Get(e);

            AddMoneyToStack(e, moneyComp.CurrentX, moneyComp.CurrentZ, moneyComp.CurrentY);
            moneyComp.CurrentX++;
            if (moneyComp.CurrentX == moneyComp.Rows)
            {
                moneyComp.CurrentX = 0;
                moneyComp.CurrentZ++;
            }
            if (moneyComp.CurrentZ == moneyComp.Cols)
            {
                moneyComp.CurrentZ = 0;
                moneyComp.CurrentY++;
            }
        }

        private void AddMoneyToStack(Entity entity, int x, int z, int y)
        {
            var moneyComp = _moneyStash.Get(entity);
            var moneyPrefab = moneyComp.MoneyPrefab;
            ref var moneyList = ref moneyComp.Money;

            var money = Object.Instantiate(moneyPrefab, _transformStash.Get(entity).Transform, true);
            var newPos = new Vector3(x * .2f * 1.2f, y * .05f* 1.2f+1f, z * 0.3f* 1.2f);
            money.transform.localPosition = newPos;
            money.transform.localScale = Vector3.zero;
            money.transform.DOScale(1, 1).SetEase(Ease.OutElastic);
            moneyList.Add(money);
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                ref var timeComponent = ref _timingStash.Get(e);
                if (Time.time - timeComponent.LastActionTime > .1f)
                {
                    AddMoneyBlock(e);
                    timeComponent.LastActionTime = Time.time;
                }
            }
        }
        
        private void AddMoneyBlock(Entity e)
        {
            ref var c = ref _moneyStash.Get(e);
            if (c.MoneyCount > 0)
            {
                AddStackOfMoney(e);
                c.MoneyCount--;
            }
        }
    }
}