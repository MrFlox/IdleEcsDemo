using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct RadiusColliderComponent : IComponent, IValidatableWithGameObject
    {
        public SphereCollider Collider;
        
        public void OnValidate(GameObject gameObject)
        {
            gameObject.TryGetComponent<SphereCollider>(out var sphereCollider);
            if (sphereCollider != null)
                Collider = sphereCollider;
        }
    }
    
    public sealed class RadiusColliderProvider : MonoProvider<RadiusColliderComponent> { }
}