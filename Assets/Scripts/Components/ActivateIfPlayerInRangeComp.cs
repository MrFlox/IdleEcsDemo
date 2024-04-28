using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ActivateIfPlayerInRangeComp : IComponent, IValidatableWithGameObject
    {
        public SphereCollider Collider;
        public MeshRenderer CircleMaterial;
       
        public void OnValidate(GameObject gameObject)
        {
            gameObject.TryGetComponent<SphereCollider>(out var sphereCollider);
            if (sphereCollider != null)
            {
                Collider = sphereCollider;
            }

        }
    }
}