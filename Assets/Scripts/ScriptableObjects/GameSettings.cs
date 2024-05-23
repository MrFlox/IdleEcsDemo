﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ScriptableObjects
{
    
    [Serializable]
    public struct BerriesSettings
    {
        [Header(header:"Berries settings")]
        public float BerryGrowthActivationDelay;
        [Range(0, .4f)]
        public float BerryMaxScale;
        [Range(0, 1f)]
        public float BerryGrowthSpeed;
        public float BerryFlySpeed;
        public float BerryFlyDelay;
    }
    
    [CreateAssetMenu(fileName = "GameSettings", menuName = "IdleGame", order = 0)]
    public class GameSettings : ScriptableObject
    {
        public GameObject GeneratorPrefab;
        public Color ActiveColor;
        public Color InactiveColor;
        public BerriesSettings BerriesSettings;
        public GameObject ResBall;
        public GameObject ResBallFromPlayer;
        public float BerryCollectableSpeed;
        public GameObject Shadow;

        public GameObject FlyingLabelPrefab;
    }
}