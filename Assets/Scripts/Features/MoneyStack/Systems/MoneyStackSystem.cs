using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.MoneyStack.Systems
{
    public class MoneyStackSystem : UpdateSystem
    {
        private Filter _filter;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<Components.MoneyStack>().Build();
            foreach (var e in _filter)
            {
                AddStackOfMoney(e);
            }
        }
        private void AddStackOfMoney(Entity e)
        {
            for (int y = 0; y < 5; y++)
            for (int x = 0; x < 5; x++)
            for (int z = 0; z < 5; z++)
                AddMoneyToStack(e, x, z, y);    
        }
        
        private void AddMoneyToStack(Entity entity,  int x, int z, int y)
        {
            var moneyComp = entity.GetComponent<Components.MoneyStack>();
            var moneyPrefab = moneyComp.MoneyPrefab;
            ref var moneyList = ref moneyComp.Money;
            
            var money = Object.Instantiate(moneyPrefab);
            money.transform.SetParent(entity.GetComponent<TransformComponent>().Transform);
            // var y = moneyList.Count / 25;
            var newPos =  new Vector3(x * .2f, y * .05f, z * 0.3f);
            money.transform.localPosition = newPos;
            moneyList.Add(money);
        }

        private void AddMoneyToStack(Entity entity)
        {
            var moneyComp = entity.GetComponent<Components.MoneyStack>();
            var moneyPrefab = moneyComp.MoneyPrefab;
            ref var moneyList = ref moneyComp.Money;
            
            var money = Object.Instantiate(moneyPrefab);
            money.transform.SetParent(entity.GetComponent<TransformComponent>().Transform);
            
            var newPos =  new Vector3(moneyList.Count * .2f, moneyList.Count * .05f, 0);
            money.transform.localPosition = newPos;
            moneyList.Add(money);
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                
            }
        }
    }
}