using System;
using System.Collections.Generic;
using Scellecs.Morpeh;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.MoneyStack.Components
{
    [Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct MoneyStack : IComponent
    {
        public int MoneyCount;
        public int CurrentX;
        public int CurrentZ;
        public int CurrentY;

        public int Cols;
        public int Rows;
        
        public GameObject MoneyPrefab;
        [SerializeField] public List<GameObject> Money;
    }
}