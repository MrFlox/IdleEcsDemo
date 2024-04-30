using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Features.Berries.Systems;
using Features.Generators.Systems;
using Features.Player.Systems;
using Features.Shared.Systems;
using Scellecs.Morpeh;
using ScriptableObjects;
using UnityEngine;
using VContainer;
using VContainer.Diagnostics;
using VContainer.Unity;

namespace DI
{
    class Init : IStartable
    {
        private readonly ScoreManager _scoreManager;

        public Init(ScoreManager scoreManager) => _scoreManager = scoreManager;

        public void Start() => _scoreManager.Init();
    }

    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private List<ScriptableObject> _systems;
        [SerializeField] private GameSettings _gameSettings;

        private IContainerBuilder _builder;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _builder = builder;
            
            RegisterSystems();
            builder.Register<ScoreManager>(Lifetime.Singleton);
            builder.RegisterInstance(_gameSettings);
            builder.RegisterEntryPoint<Init>();
        }
    
        private void Register<T>() => _builder.Register<T>(Lifetime.Singleton);
        
        private void RegisterSystems()
        {
            Register<ActivateBerriesSystem>();
            Register<AddGeneratorsSystem>();
            Register<SimpleFlyingBerrySystem>();
            Register<HilightObjectIfPlayerInRangeSystem>();
            Register<PlayerInputSystem>();
            Register<DeleteBerriesSystem>();
            Register<PlayerAnimationSystem>();
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
}