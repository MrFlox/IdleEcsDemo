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
    [CreateAssetMenu(menuName = "ECS/Systems/" + nameof(DeleteBerriesSystem))]
    public class DeleteBerriesSystem: UpdateSystem 
    {
        private Filter _filter;
        private Stash<DeleteComponent> _moveStash;
        
        public override void OnAwake()
        {
            _filter = World.Filter.With<DeleteComponent>().Build();
            _moveStash = World.GetStash<DeleteComponent>();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                var gameObject = entity.GetComponent<PositionOnStage>().Transform.gameObject;
                
                // entity.GetComponent<ResourceGeneratorComponent>()
                //     ._Berries
                //     .Remove(entity.GetComponent<PositionOnStage>().Transform);
                Destroy(gameObject);
                World.RemoveEntity(entity);
            }
        }
    }
}