using System.Collections.Generic;
using DG.Tweening;
using Scellecs.Morpeh;
using UnityEngine;

namespace Features.MoneyStack.Services
{
    public static class MoneyFactory
    {
        public static void CreateMoneyBlock(Entity entity, Vector3Int pos, GameObject moneyPrefab, List<GameObject> moneyList, Transform entityTranform)
        {
            var money = Object.Instantiate(moneyPrefab, entityTranform, true);
            var newPos = new Vector3(pos.x * .2f * 1.2f, pos.y * .05f * 1.2f + .3f, pos.z * 0.3f * 1.2f);
            money.transform.localPosition = newPos;
            money.transform.localScale = Vector3.zero;
            money.transform.DOScale(1, 1).SetEase(Ease.OutElastic);
            moneyList.Add(money);
        }
    }
}