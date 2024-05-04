using DG.Tweening;
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

        private void UpdateInterface()
        {
            DOVirtual.Int(_counter, _scoreManager.GetCounter(), .2f, (x) => Label.text = x.ToString());
            // Label.transform.DOScale(1.5f, .3f)
            //     .OnComplete(() => Label.transform.DOScale(1, .3f));
        }
    }
}
