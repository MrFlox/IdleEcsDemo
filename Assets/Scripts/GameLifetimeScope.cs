using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;


public class TempClass
{
    public void HelloWorld()
    {
        Debug.Log("Hello world");
    }
}

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private List<ScriptableObject> _systems;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<TempClass>(Lifetime.Singleton);        
        foreach (var system in _systems)
        {
            builder.RegisterInstance(system);
        }
    }
}