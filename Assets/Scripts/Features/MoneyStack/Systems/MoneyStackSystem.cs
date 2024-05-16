using System.Collections.Generic;
using Features.MoneyStack.Services;
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

            var newPos = new Vector3Int(moneyComp.CurrentX, moneyComp.CurrentY, moneyComp.CurrentZ);
            AddMoneyToStack(e, newPos);
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

        private void AddMoneyToStack(Entity entity, Vector3Int pos)
        {
            var moneyComp = _moneyStash.Get(entity);
            var moneyPrefab = moneyComp.MoneyPrefab;
            ref var moneyList = ref moneyComp.Money;

            InstantiateMoney(entity, pos, moneyPrefab, moneyList);
        }

        private void InstantiateMoney(Entity entity, Vector3Int pos, GameObject moneyPrefab,
            List<GameObject> moneyList)
        {
            MoneyFactory.CreateMoneyBlock(entity, pos, moneyPrefab, moneyList,
                _transformStash.Get(entity).Transform);
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