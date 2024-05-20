using System;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;

namespace Features.Shared.Components
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ResourcesStorageComponent: IComponent
    {
        public int Count;
        public int SpawnCounter;
        public Entity CurrentEntity;
    }
}