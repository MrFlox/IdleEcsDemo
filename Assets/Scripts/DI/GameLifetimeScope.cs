using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Berries.Systems;
using Generators.Systems;
using Player.Systems;
using Scellecs.Morpeh;
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


class Init : IStartable
{
    private readonly Manager _manager;

    public Init(Manager manager) => _manager = manager;

    public void Start() => _manager.Init();
}

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private List<ScriptableObject> _systems;
    [SerializeField] private GameSettings _gameSettings;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<SimpleFeature>(Lifetime.Singleton);
        RegisterSystems(builder);
        builder.Register<Manager>(Lifetime.Singleton);

        builder.RegisterInstance(_gameSettings);
        
        builder.RegisterEntryPoint<Init>();
        
    }
    
    private void RegisterSystems(IContainerBuilder builder)
    {
        builder.Register<ActivateBerriesSystem>(Lifetime.Singleton);
        builder.Register<TempClass>(Lifetime.Singleton);
        builder.Register<AddGeneratorsSystem>(Lifetime.Singleton);
        builder.Register<SimpleFlyingBerrySystem>(Lifetime.Singleton);
        builder.Register<HilightObjectIfPlayerInRangeSystem>(Lifetime.Singleton);
        builder.Register<PlayerInputSystem>(Lifetime.Singleton);
        builder.Register<DeleteBerriesSystem>(Lifetime.Singleton);
        builder.Register<PlayerAnimationSystem>(Lifetime.Singleton);
    }
    
    private static void RegisterAllSystemUsingAssembly(IContainerBuilder builder)
    {

        var assembly = Assembly.GetExecutingAssembly();

        // Находим все типы, которые реализуют интерфейс ISystem
        var systemTypes = assembly.GetTypes()
            .Where(t => typeof(ISystem).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);

        // Регистрируем каждый тип в контейнере VContainer
        foreach (var type in systemTypes)
        {
            builder.Register(type, Lifetime.Singleton);
        }
    }
}