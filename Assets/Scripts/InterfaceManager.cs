using System;
using Systems;
using UnityEngine;
using VContainer.Unity;

public class InterfaceManager: IStartable
{
    public event Action OnUpdateInterface;
    
    private readonly GeneratorRadiusDrawerSystem _system;
    private int _counter;
    
    public InterfaceManager(GeneratorRadiusDrawerSystem system) => _system = system;

    public void Start() => _system.OnActivateBerry += OnActivateBerryHandler;

    private void OnActivateBerryHandler()
    {
        Debug.Log("Activate Berry!!!!");
        _counter++;
        OnUpdateInterface?.Invoke();
    }
    
    public int GetCounter() => _counter;
}