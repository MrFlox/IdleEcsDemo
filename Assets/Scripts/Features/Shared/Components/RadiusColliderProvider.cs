using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using TriInspector;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Shared.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct RadiusColliderComponent : IComponent, IValidatableWithGameObject
    {
        [SerializeField]
        private SphereCollider Collider;

        [ShowInInspector]
        public float Radius => Collider != null ? Collider.radius : 0;

        public void OnValidate(GameObject gameObject)
        {
            gameObject.TryGetComponent<SphereCollider>(out var sphereCollider);
            if (sphereCollider != null)
                Collider = sphereCollider;
        }
    }

    public sealed class RadiusColliderProvider : MonoProvider<RadiusColliderComponent> { }
}