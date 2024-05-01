using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Generators.Providers
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ResourceGeneratorComponent : IComponent, IValidatableWithGameObject
    {
        public List<Transform> _Berries;
        public ResourceStates State;
        public int LastIndex;
        public enum ResourceStates
        {
            None,
            ReadyToCollect,
            Collecting,
            Done
        }

        public float LastTime;

        public void OnValidate(GameObject gameObject)
        {
            _Berries = new List<Transform>();
            foreach (Transform child in gameObject.transform)
            {
                child.TryGetComponent<Transform>(out var berry);
                if (berry != null && berry.name.Contains("Berry"))
                    _Berries.Add(berry);
            }
        }
    }
    
    public sealed class ResourceGeneratorProvider : MonoProvider<ResourceGeneratorComponent> { }
}