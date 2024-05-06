using VContainer.Unity;

namespace UI.MVPTest
{
    public class TestPresenter: IStartable
    {
        private readonly TestModel _model;
        private  readonly TestView _view;

        public TestPresenter(TestModel model, TestView view)
        {
            _model = model;
            _view = view;
        }

        public void Start()
        {
            _view.OnClickEvent += OnClickEventHandler;
            _model.OnValueChanged += OnValueChangeHandler;
        }
        
        private void OnClickEventHandler() => 
            _model.Increment();

        private void OnValueChangeHandler() => 
            _view.SetValue(_model.Value);
    }
}