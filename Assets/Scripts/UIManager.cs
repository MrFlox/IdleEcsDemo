using Systems;
using TMPro;
using UnityEngine;
using VContainer;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Label;
    private HilightObjectIfPlayerInRangeSystem _system;
    private int _counter;
    private Manager _manager;

    [Inject] private void Construct(HilightObjectIfPlayerInRangeSystem generatorsSystem, Manager manager)
    {
        _manager = manager;
        _system = generatorsSystem;
    }

    private void Start() => 
        _manager.OnUpdateInterface += UpdateInterface;

    private void UpdateInterface() => 
        Label.text = _manager.GetCounter().ToString();
}
