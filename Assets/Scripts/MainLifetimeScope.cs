using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    [SerializeField] private UIManager _uiManager;
    protected override void Configure(IContainerBuilder builder)
    {
    }
}