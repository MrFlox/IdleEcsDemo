using Features.Shared.Components;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;


namespace Features.Shared.Providers
{
    [AddComponentMenu("Idle/" + nameof(LabelContainerComponent))]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public sealed class LabelContainerProvider : MonoProvider<LabelContainerComponent>
    {
        
    }
}