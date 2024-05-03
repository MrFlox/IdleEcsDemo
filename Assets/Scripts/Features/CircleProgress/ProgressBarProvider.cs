using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Features.CircleProgress
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ProgressBarComponent:IComponent
    {
        public Image ProgressBarImage;
        public Canvas Canvas;
        public Camera Camera;
    }
    
    public sealed class ProgressBarProvider: MonoProvider<ProgressBarComponent> { }
}