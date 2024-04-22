using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct FloatingComponent : IComponent
    {
        public float InitialY;
        public MoveDirection Direction;
        public float MoveSpeed;
        
        public enum MoveDirection
        {
            Up,
            Down
        }

    }
}