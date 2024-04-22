using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TriInspector;
using UnityEngine;

public class BallContainer : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    private bool _collecting;

    [Button]
    private void AddBall()
    {
        var ball = Instantiate(_ballPrefab);
        ball.transform.position = transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        _collecting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_collecting) return;
            _collecting = true;

            StarCollecting().Forget();
        }
    }

    private async UniTask StarCollecting()
    {
        while (_collecting)
        {
            AddBall();
            await UniTask.Delay(TimeSpan.FromMilliseconds(400));
        }
    }
}
