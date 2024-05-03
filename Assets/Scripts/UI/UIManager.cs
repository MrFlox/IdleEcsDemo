using TMPro;
using UnityEngine;
using VContainer;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text Label;
        private int _counter;
        private ScoreManager _scoreManager;

        [Inject] private void Construct(ScoreManager scoreManager) => _scoreManager = scoreManager;

        private void Start() => 
            _scoreManager.OnUpdateInterface += UpdateInterface;

        private void UpdateInterface() => 
            Label.text = _scoreManager.GetCounter().ToString();
    }
}
