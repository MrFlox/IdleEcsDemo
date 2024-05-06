using VContainer;
using VContainer.Unity;

namespace UI.MVPTest
{
    public class MVPInstaller
    {
        private readonly IContainerBuilder _builder;
        private readonly TestView _view;

        public MVPInstaller(IContainerBuilder builder, TestView view)
        {
            _builder = builder;
            _view = view;
        }

        public void Configure()
        {
            _builder.RegisterInstance(_view);
            _builder.Register<TestModel>(Lifetime.Singleton);
            _builder.RegisterEntryPoint<TestPresenter>();
        }
    }
}