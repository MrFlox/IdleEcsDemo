using System.Collections.Generic;
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
    public struct ResourceGeneratorComponent : IComponent, IValidatableWithGameObject
    {
        public List<PositionOnStageProvider> BerriesProviders;
        public ResourceStates State;

        public enum ResourceStates
        {
            None,
            ReadyToCollect,
            Collecting,
            Done
        }
        
        
        public void OnValidate(GameObject gameObject)
        {
            BerriesProviders = new List<PositionOnStageProvider>();
            foreach (Transform child in gameObject.transform)
            {
                child.TryGetComponent<PositionOnStageProvider>(out var berry);
                if (berry != null)
                    BerriesProviders.Add(berry);
            }
        }
    }
    
    public sealed class ResourceGeneratorProvider : MonoProvider<ResourceGeneratorComponent> { }
}