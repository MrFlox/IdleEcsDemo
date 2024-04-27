using System;
using Systems;
using UnityEngine;

public class Manager
{
    public event Action OnUpdateInterface;
    
    private readonly HilightObjectIfPlayerInRangeSystem _system;
    private int _counter;
    
    public Manager(HilightObjectIfPlayerInRangeSystem system)
    {
        _system = system;
    }

    public void Init()
    {
        // _system.OnActivateBerry += OnActivateBerryHandler;
    }

    private void OnActivateBerryHandler()
    {
        Debug.Log("Activate Berry!!!!");
        _counter++;
        OnUpdateInterface?.Invoke();
    }
    
    public int GetCounter() => _counter;
}