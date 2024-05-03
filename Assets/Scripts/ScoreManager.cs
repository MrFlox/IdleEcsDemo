using System;
using Features.Berries.Systems;
using UnityEngine;

public class ScoreManager
{
    public event Action OnUpdateInterface;
    
    private readonly ActivateBerriesSystem _system;
    private int _counter;
    
    public ScoreManager(ActivateBerriesSystem system) => _system = system;

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