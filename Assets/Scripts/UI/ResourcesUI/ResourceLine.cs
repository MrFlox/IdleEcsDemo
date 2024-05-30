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

        public void SetResourceType(ResourceGeneratorComponent.ResourceType type)
        {
            _sprite.color = GetColorByType(type);
        }

        public void SetAmount(int pairValue)
        {
            _label.text = pairValue.ToString();
        }

        private Color GetColorByType(ResourceGeneratorComponent.ResourceType type)
        {
            switch (type)
            {
                case ResourceGeneratorComponent.ResourceType.Green:
                    return Color.green;
                case ResourceGeneratorComponent.ResourceType.Red:
                    return Color.red;
                case ResourceGeneratorComponent.ResourceType.Yellow:
                    return Color.yellow;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
