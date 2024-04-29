using System;
using Generators.Systems;
using Systems;
using UnityEngine;

public class Manager
{
    public event Action OnUpdateInterface;
    
    private readonly ActivateBerriesSystem _system;
    private int _counter;
    
    public Manager(ActivateBerriesSystem system)
    {
        _system = system;
    }

    public void Init() => 
        _system.OnBerryActivated += OnActivateBerryHandler;

    private void OnActivateBerryHandler()
    {
        Debug.Log("Activate Berry!!!!");
        _counter++;
        OnUpdateInterface?.Invoke();
    }
    
    public int GetCounter() => _counter;
}