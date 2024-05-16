﻿using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;

namespace Features.ResourceCounter.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ResourceCounterUiComponent : IComponent
    {
        public TMP_Text Label;
    }
}