using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;

[SelectionBase]
public class Generator : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;
    private List<GameObject> _berries = new();
    private bool _collecting;
    private Coroutine _collecting1;
    private GameObject _berrie;
    private GameObject _lastBerrie;
    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.TryGetComponent<Berry>(out var berry);
            if (berry != null)
                _berries.Add(berry.gameObject);
        }
    }

    private async void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            await StartCollect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopCollect();
        }
    }
    
    private async UniTask StartCollect()
    {
        if (_collecting) return;
        _collecting = true;

        await StartCollecting();
        
    }

    private async UniTask StartCollecting()
    {
        while (_collecting && _berries.Count > 0)
        {
            await Collect();
            await UniTask.Delay(TimeSpan.FromMilliseconds(400));
            if (_berries.Count == 0) _collecting = false;
        }
    }

    private void StopCollect() => _collecting = false;

    [Button] 
    private void ChangeColorOfCircle()
    {
        _mesh.material.color = Color.red;
    }
    
    [Button]    
    private async UniTask Collect()
    {
        if (_berries.Count == 0) return;
        _berrie = _berries[^1];
        if(_lastBerrie == _berrie) return;
        _lastBerrie = _berrie;
        var scale = _berrie.transform.localScale;
        // lastBerrie.transform.DOScaleY(1, .4f);
        // lastBerrie.GetComponent<MeshRenderer>().material.DOFade(0, .4f);
        var newPos = _berrie.transform.position.y + 1;
        await _berrie.transform.DOMoveY(newPos, .4f).SetEase(Ease.OutExpo).ToUniTask();
        await UniTask.Yield();
        _berries.Remove(_berrie);
        Destroy(_berrie);
    }
}
