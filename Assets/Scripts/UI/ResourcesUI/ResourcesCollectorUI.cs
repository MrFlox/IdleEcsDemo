using System;
using System.Collections;
using System.Collections.Generic;
using Features.CollectingPoint.Components;
using UI.ResourcesUI;
using UnityEngine;

[Serializable]
public class ResourcesCollectorUI : MonoBehaviour
{
    [SerializeField] private List<ResourceLine> _lines;
    public void UpdateValues(List<ResourceAmount> cNeededResourcesList)
    {
        foreach (var line in _lines)
            line.gameObject.SetActive(false);
        
        int index = 0;
        foreach (var resourceAmount in cNeededResourcesList)
        {
            _lines[index].gameObject.SetActive(true);
            _lines[index].SetAmount(resourceAmount.Amount);
            _lines[index].SetResourceType(resourceAmount.Type);
            index++;
        }
    }
}
