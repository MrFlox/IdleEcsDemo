using Systems;
using TMPro;
using UnityEngine;
using VContainer;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text Label;
    private GeneratorRadiusDrawerSystem _manager;
    private int _counter;
    
    [Inject] private void Construct(GeneratorRadiusDrawerSystem manager) => _manager = manager;

    private void Start() => 
        _manager.OnActivateBerry += UpdateInterface;

    private void UpdateInterface() => 
        Label.text = _counter++.ToString();
}
