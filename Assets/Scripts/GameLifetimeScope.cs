using System.Collections.Generic;
using Systems;
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

        RegisterSystems(builder);
        
        builder.Register<TempClass>(Lifetime.Singleton);        
        foreach (var system in _systems)
        {
            builder.RegisterInstance(system);
        }
    }
    
    private void RegisterSystems(IContainerBuilder builder)
    {
        builder.Register<SimpleFeature>(Lifetime.Singleton);
        
        builder.Register<AddGeneratorsSystem>(Lifetime.Singleton);
        builder.Register<SimpleFlyingBerrySystem>(Lifetime.Singleton);
        builder.Register<GeneratorRadiusDrawerSystem>(Lifetime.Singleton);
        builder.Register<PlayerInputSystem>(Lifetime.Singleton);
        builder.Register<DeleteBerriesSystem>(Lifetime.Singleton);
        builder.Register<PlayerAnimationSystem>(Lifetime.Singleton);
    }
}