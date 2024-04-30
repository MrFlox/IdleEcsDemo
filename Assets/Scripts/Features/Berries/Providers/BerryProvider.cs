using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Features.Berries.Providers
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct BerryComponent : IComponent
    {
        public bool Collected;
        public float Speed;
        public Entity Entity;
    }

    public sealed class BerryProvider : MonoProvider<BerryComponent> { }
}