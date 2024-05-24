using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Berries.Components
{
    /// <summary>
    /// Компонент, помечающий куст, как растущий
    /// </summary>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct GrowingBushComponent : IComponent
    {

    }
}