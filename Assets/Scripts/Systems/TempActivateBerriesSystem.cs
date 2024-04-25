using System.Threading.Tasks;
using Components;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Systems;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Systems
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(TempActivateBerriesSystem))]
    public sealed class TempActivateBerriesSystem : UpdateSystem {
        private Filter _filter;
        private Stash<ResourceGeneratorComponent> _moveStash;

        public override  void OnAwake()
        {

            _filter = World.Filter.With<ResourceGeneratorComponent>().Build();
            _moveStash = World.GetStash<ResourceGeneratorComponent>();

            ref var entity = ref _moveStash.Get(_filter.First());

            ActivateBerries(entity);
        }
        
        private async void ActivateBerries(ResourceGeneratorComponent entity)
        {
            foreach (var berry in entity._Berries)
            {
                await ActivateBerryCollection(berry);
            }
        }
        
        private async UniTask ActivateBerryCollection(Transform berry)
        {
            Entity newEntity = World.CreateEntity();
            newEntity.AddComponent<PositionOnStage>().Transform = berry;
            newEntity.AddComponent<BerryComponent>().Speed = Random.value;
            World.Commit();
            await UniTask.WaitForSeconds(1);
        }

        public override void OnUpdate(float deltaTime) {
        }
    }
}