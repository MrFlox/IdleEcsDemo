﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        // RegisterAllSystemUsingAssembly(builder);

        
        builder.Register<SimpleFeature>(Lifetime.Singleton);
        
        builder.Register<AddGeneratorsSystem>(Lifetime.Singleton);
        builder.Register<SimpleFlyingBerrySystem>(Lifetime.Singleton);
        builder.Register<GeneratorRadiusDrawerSystem>(Lifetime.Singleton);
        builder.Register<PlayerInputSystem>(Lifetime.Singleton);
        builder.Register<DeleteBerriesSystem>(Lifetime.Singleton);
        builder.Register<PlayerAnimationSystem>(Lifetime.Singleton);
        
        // builder.Register<InterfaceManager>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InterfaceManager>();
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