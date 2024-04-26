using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Systems
{
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
                Object.Destroy(gameObject);
                World.RemoveEntity(entity);
            }
        }
    }
}