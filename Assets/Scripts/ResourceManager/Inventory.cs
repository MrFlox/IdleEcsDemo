using System;
using System.Collections.Generic;
using Features.Generators.Providers;
using static Features.Generators.Providers.ResourceGeneratorComponent;

namespace ResourceManager
{
    public class Inventory
    {
        public event Action OnUpdate;
        public Dictionary<ResourceType, int> CollectedResources = new();

        public void AddResource(ResourceType type, int amount)
        {
            if (!CollectedResources.TryAdd(type, amount))
                CollectedResources[type] += amount;
            
            OnUpdate?.Invoke();
        }

        public void SpendResource(ResourceType type, int amount)
        {
            if (!CollectedResources.ContainsKey(type)) return;
            if (CollectedResources[type] < amount)
                return;

            CollectedResources[type] -= amount;

            if (CollectedResources[type] == 0)
                CollectedResources.Remove(type);
            
            OnUpdate?.Invoke();
        }
    }
}