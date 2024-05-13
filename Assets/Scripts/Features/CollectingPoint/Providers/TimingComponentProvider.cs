using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Features.CollectingPoint.Providers
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TimingComponentProvider: MonoProvider<Components.TimingComponent>
    {
        
    }
}