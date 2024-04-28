using Berries.Providers;
using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;

namespace Berries.Systems
{
    public class SimpleFlyingBerrySystem: SimpleSystem<BerryComponent, PositionOnStage>
    {

        protected override void Process(Entity entity, ref BerryComponent first, ref PositionOnStage second, in float deltaTime)
        {
            if(!entity.Has<BerryComponent>()) return;
                
            ref var transform = ref second.Transform;
            
            var pos = transform.position;
            pos.y += first.Speed * deltaTime;
            transform.position = pos;
            
            if (pos.y > 4f)
                DeleteEntity(entity);
        }
        
        private static void DeleteEntity(Entity entity)
        {
            if (!entity.Has<DeleteComponent>())
                entity.AddComponent<DeleteComponent>();
        }
    }
}