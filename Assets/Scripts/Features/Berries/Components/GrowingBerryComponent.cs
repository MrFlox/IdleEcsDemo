﻿using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Berries.Components
{
    /// <summary>
    /// Компонент одной растущей ягоды
    /// </summary>
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct GrowingBerryComponent : IComponent
    {
        public Transform Transform;
        public int Index;
        public bool Finished;
        public Entity Entity;
    }
}