using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private SystemRegistration _systemRegistration;
        
        protected override void Configure(IContainerBuilder builder)
        {
            _systemRegistration = new SystemRegistration(builder);
            _systemRegistration.Register();
            
            builder.Register<ScoreManager>(Lifetime.Singleton);
            builder.RegisterInstance(_gameSettings);
            builder.RegisterEntryPoint<Init>();
        }
    }

}