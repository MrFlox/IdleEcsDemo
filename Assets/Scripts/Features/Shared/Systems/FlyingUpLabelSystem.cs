using Features.Shared.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Features.Shared.Systems
{
    public class FlyingUpLabelSystem : UpdateSystem
    {
        private Filter _filter;
        private Stash<TransformComponent> _transformStash;
        private Camera _camera;
        
        public override void OnAwake()
        {
            var cameraEntity = World.Filter.With<MainCameraComponent>().Build().First();
            _camera = cameraEntity.GetComponent<MainCameraComponent>().Camera;
            
            _filter = World.Filter.With<FlyingUpLabelComponent>().With<TransformComponent>().Build();
            _transformStash = World.GetStash<TransformComponent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _filter)
            {
                var newPos = e.GetComponent<FlyingUpLabelComponent>().Position += Vector3.up * deltaTime * 3;
                _transformStash.Get(e).Transform.position =  _camera.WorldToScreenPoint(newPos + Vector3.zero);

                if (newPos.y > 3)
                {
                    Object.Destroy(_transformStash.Get(e).Transform.gameObject);
                    World.RemoveEntity(e);
                }
            }
        }

    }
}