using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TriInspector;
using UnityEngine;

public class PileOfBlocks : MonoBehaviour
{
    [SerializeField] private PileOfBlocks _targetPile;
    [SerializeField] private List<GameObject> _blocks = new();
    private const float BlockHeight = 0.65f-0.39f;
    private void Start()
    {
        InitBlocks();
    }
    
    private void InitBlocks()
    {
        foreach (Transform block in transform)
            _blocks.Add(block.gameObject);
    }


    [Button]
    private void MoveBlockToAnotherPile()
    {
        var lastBlock = _blocks[^1];
        _blocks.Remove(lastBlock);

        _targetPile.AddBlock(lastBlock);
    }
    
    private async void AddBlock(GameObject lastBlock)
    {
        var newPos = transform.TransformPoint(Vector3.zero);
        newPos.y = BlockHeight * _blocks.Count;
        lastBlock.transform.DOLocalRotateQuaternion(transform.localRotation, .4f);
        await MoveToNewPosition(lastBlock, newPos);
        lastBlock.transform.SetParent(transform, true);
        _blocks.Add(lastBlock);
    }
    private static async UniTask MoveToNewPosition(GameObject lastBlock, Vector3 newPos)
    {
        await lastBlock.transform.DOMove(newPos, 1f).ToUniTask();
    }
}
