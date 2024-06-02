using System;
using System.Collections.Generic;
using Features.CollectingPoint.Components;
using Scellecs.Morpeh;
using UI.ResourcesUI;
using Unity.IL2CPP.CompilerServices;

namespace Features.Shared.Components
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ResourcesStorageComponent: IComponent
    {
        public List<ResourceAmount> Resources;
        public ResourcesCollectorUI Ui;
    }
}