using System;
using Features.Generators.Providers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ResourcesUI
{
    public class ResourceLine : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TMP_Text _label;

        public void SetResourceType(ResourceType type)
        {
            _sprite.color = GetColorByType(type);
        }

        public void SetAmount(int pairValue)
        {
            _label.text = pairValue.ToString();
        }

        private Color GetColorByType(ResourceType type)
        {
            switch (type)
            {
                case ResourceType.Green:
                    return Color.green;
                case ResourceType.Red:
                    return Color.red;
                case ResourceType.Yellow:
                    return Color.yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
