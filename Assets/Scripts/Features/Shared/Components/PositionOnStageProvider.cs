using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Shared.Components
{
    public sealed class PositionOnStageProvider : MonoProvider<PositionOnStage> { }

    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct PositionOnStage : IComponent, IValidatableWithGameObject
    {
        public Transform Transform;
        
        public void OnValidate(GameObject gameObject) => Transform = gameObject.transform;
    }

}

