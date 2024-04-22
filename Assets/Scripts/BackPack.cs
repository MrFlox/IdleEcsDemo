using System.Collections.Generic;
using DG.Tweening;
using TriInspector;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    private const float BlockHeight = 0.65f-0.39f;
    [SerializeField] private Transform _blocksContainer;
    [SerializeField] private GameObject _blockPrefab;
    private List<GameObject> _blocks = new();
    private Vector3 _startPosition = new(0, 0.39f, -0.39f);
    
    
    
    [Button]
    private void AddBlock()
    {
        GameObject newBlock = GenerateBlock();
        var newPos = _startPosition;
        _startPosition.y = BlockHeight * _blocks.Count;
        newBlock.transform.position = _startPosition;
        newBlock.transform.SetParent(_blocksContainer, false);
        newBlock.transform.localScale = Vector3.zero;
        newBlock.transform.DOScale(_blockPrefab.transform.localScale, .4f).SetEase(Ease.OutElastic);
        _blocks.Add(newBlock);
    }

    [Button]
    private void TeleportToPosition()
    {

    }
    
    private GameObject GenerateBlock() => Instantiate(_blockPrefab);
}