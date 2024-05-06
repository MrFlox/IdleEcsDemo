using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MVPTest
{
    public class TestView : MonoBehaviour
    {
        public event Action OnClickEvent;
        
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _value;
        [SerializeField] private Button _button;

        private void Start() => _button.onClick.AddListener(OnClickHandler);

        private void OnClickHandler() => OnClickEvent?.Invoke();

        public void SetHeader(string text) => _header.text = text;
        
        public void SetValue(int value) => _value.text = value.ToString();
    }
}
