using System;
using System.Collections.Generic;
using Features.Generators.Providers;
using ResourceManager;
using UnityEngine;
using VContainer;

namespace UI.ResourcesUI
{
    public class ResourceStackRenderer : MonoBehaviour
    {
        private Inventory _inventory;
        [SerializeField] private ResourceLine _resourceLinePrefab;


        [Inject] private void Construct(Inventory inventory)
        {
            _inventory = inventory;
        }
        
        private Dictionary<ResourceType, ResourceLine> _lines = new();
        
        private void Start()
        {
            _inventory.OnUpdate += OnUpdateHandler;

            OnUpdateHandler();
        }
        
        private void OnUpdateHandler()
        {
            foreach (var pair in _inventory.CollectedResources)
            {
                if (_lines.ContainsKey(pair.Key))
                {
                    _lines[pair.Key].SetAmount(pair.Value);
                }
                else
                {
                    var newLine = Instantiate(_resourceLinePrefab);
                    newLine.transform.SetParent(transform);
                    newLine.SetResourceType(pair.Key);
                    newLine.SetAmount(pair.Value);
                    _lines.Add(pair.Key, newLine);
                }
            }            
        }
    }
}
