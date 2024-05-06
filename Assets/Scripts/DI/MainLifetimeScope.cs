using UI;
using UI.MVPTest;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class MainLifetimeScope : LifetimeScope
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private TestView _view;
        
        protected override void Configure(IContainerBuilder builder)
        {
            new MVPInstaller(builder, _view).Configure();
        }
    }
}