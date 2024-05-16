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
    public struct ParabolaDropFromPlayerComponent : IComponent
    {
        public bool Activated;
        public float Speed;

        [Range(0, 1f)]
        public float Time;

        public Transform StartPosition;

        public Transform EndPosition
        {
            get { return _endPosition; }
            set
            {
                _endPosition = value;
                _endPositionVector3 = _endPosition.position;
            }
        }

        private Transform _endPosition;
        private Vector3 _endPositionVector3;

        public Vector3 EndPositionValue
        {
            get
            {
                if (_endPosition != null) return _endPosition.position;
                return _endPositionVector3;
            }
        }

        public bool Finished;
    }
}