using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct BallsGeneratorComponent : IComponent
    {
        [FormerlySerializedAs("_ballPrefab")] public GameObject BallPrefab;
    }
    
    public sealed class BallsGeneratorProvider : MonoProvider<BallsGeneratorComponent> { }
}