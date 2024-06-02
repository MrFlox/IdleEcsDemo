using System;
using System.Collections.Generic;
using Features.CollectingPoint.Components;
using UnityEngine;

namespace UI.ResourcesUI
{
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
        public void UpdateValuesTwo(List<ResourceAmount> cNeededResourcesList, List<ResourceAmount> c1Resources)
        {
        
            int index = 0;
            foreach (var resourceAmount in cNeededResourcesList)
            {
                var value = c1Resources.Find(x => x.Type == resourceAmount.Type);
                int contains = 0;
                if (value != null)
                {
                    contains = value.Amount;
                }
                _lines[index].SetAmount(resourceAmount.Amount - contains );
                _lines[index].SetResourceType(resourceAmount.Type);
                index++;
            }
        }
    }
}
