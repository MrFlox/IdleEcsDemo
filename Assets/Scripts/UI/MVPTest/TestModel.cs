using System;

namespace UI.MVPTest
{
    public class TestModel
    {
        public event Action OnValueChanged;
        public int Value = 0;

        public void Increment()
        {
            Value++;
            OnValueChanged?.Invoke();
        }
    }
}