using Systems;
using TMPro;
using UnityEngine;
using VContainer;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Label;
    private HilightObjectIfPlayerInRangeSystem _system;
    private int _counter;
    private Manager _uimanager;

    [Inject] private void Construct(HilightObjectIfPlayerInRangeSystem generatorsSystem, Manager manager)
    {
        _uimanager = manager;
        _system = generatorsSystem;
    }

    private void Start()
    {
        _uimanager.OnUpdateInterface += UpdateInterface;
        // _system.OnActivateBerry += UpdateInterface;
    }

    private void UpdateInterface() => 
        Label.text = _uimanager.GetCounter().ToString();
}
