using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "IdleGame", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public GameObject GeneratorPrefab;
        public Color ActiveColor;
        public Color InactiveColor;
    }
}