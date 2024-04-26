using Components;
using Cysharp.Threading.Tasks;
using Scellecs.Morpeh;
using UnityEngine;

namespace Systems
{
    class BerryActivator
    {
        private readonly World _world;

        public BerryActivator(World world)
        {
            _world = world;
        }

        public void DeactivateGenerator(GeneratorComponent generator)
        {
            generator.CircleMaterial.material.color = Color.red;
        }

        public void ActivateGenerator(GeneratorComponent generator,
            ref ResourceGeneratorComponent resourceComponent)
        {
            generator.CircleMaterial.material.color = Color.green;

            if (resourceComponent.State != ResourceGeneratorComponent.ResourceStates.Collecting)
            {
                resourceComponent.State = ResourceGeneratorComponent.ResourceStates.ReadyToCollect;
            }
        }

        public async void ActivateBerries(ResourceGeneratorComponent entity)
        {
            foreach (var berry in entity._Berries)
            {
                ActivateBerryCollection(berry);
                // await ActivateBerryCollection(berry);
                // await UniTask.WaitForSeconds(1);
            }
           
        }

        private void ActivateBerryCollection(Transform berry)
        {
            Entity newEntity = _world.CreateEntity();
            newEntity.AddComponent<PositionOnStage>().Transform = berry;
            ref var berryComponent = ref newEntity.AddComponent<BerryComponent>();
            berryComponent.Speed = Random.value + .2f;
            berryComponent.Entity = newEntity;
        }
    }
}