using System;
using System.Collections.Generic;
using Features.Generators.Providers;
using Scellecs.Morpeh;
using TMPro;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.CollectingPoint.Components
{

    [Serializable]
    public class ResourceAmount
    {
        public ResourceGeneratorComponent.ResourceType Type;
        public int Amount;
        public int SpawnCounter;
    }
    
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct BuildForResourcesComponent : IComponent
    {
        public TMP_Text Text;
        public int NeededResources;
        public int ResourcesCount;
        public GameObject Result;
        public bool Activated;

        public List<ResourceAmount> NeededResourcesList;
        
        public ResourcesCollectorUI Collector;
    }
}