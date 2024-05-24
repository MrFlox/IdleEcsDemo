﻿using System;
using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Shared.Components
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct FlyingUpLabelComponent : IComponent
    {
        public TMP_Text Label;
        public Vector3 Position;
    }
}