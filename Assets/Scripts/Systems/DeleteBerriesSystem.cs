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

        public override void OnAwake()
        {
            _filter = World.Filter.With<DeleteComponent>().Build();
        }
        
        public override void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in _filter)
            {
                var gameObject = entity.GetComponent<PositionOnStage>().Transform.gameObject;
                Destroy(gameObject);
                World.RemoveEntity(entity);
            }
        }
    }
}