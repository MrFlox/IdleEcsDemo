using Features.Shared.Components;

namespace Features.Shared.Providers
{
    using Scellecs.Morpeh.Providers;
    using Unity.IL2CPP.CompilerServices;

    namespace Features.Shared.Providers
    {
        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
        public sealed class ParabolaDropProvider : MonoProvider<ParabolaDropComponent> { }
    }
}