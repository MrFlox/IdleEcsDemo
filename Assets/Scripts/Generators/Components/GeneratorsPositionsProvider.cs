using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Generators.Components
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct GeneratorsPositionsComponent : IComponent, IValidatableWithGameObject
    {
       public List<Vector3> Positions;

       public void OnValidate(GameObject gameObject)
        {
            Positions = new List<Vector3>();
            foreach (Transform child in gameObject.transform)
            {
                child.TryGetComponent<Transform>(out var position);
                if (position != null)
                    Positions.Add(position.position);
            }
        }
    }
    
    public sealed class GeneratorsPositionsProvider : MonoProvider<GeneratorsPositionsComponent> { }
}