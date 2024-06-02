using System;
using System.Collections.Generic;
using Features.Generators.Providers;
using Scellecs.Morpeh;
using TMPro;
using UI.ResourcesUI;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Features.CollectingPoint.Components
{

    [Serializable]
    public class ResourceAmount
    {
        public ResourceType Type;
        public int Amount;
        public int SpawnCounter;
        public Entity CurrentEntity;
    }
    
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct BuildForResourcesComponent : IComponent
    {
        public GameObject Result;
        public List<ResourceAmount> NeededResourcesList;
        public ResourcesCollectorUI Collector;
    }
}