using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.Shared.Components
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ParabolaDropComponent : IComponent
    {
        public bool Activated;
        public float Speed;

        [Range(0, 1f)]
        public float Time;

        public Vector3 StartPosition;
        public Vector3 EndPosition;
        public bool Finished;
        
        
    }
}