﻿using Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Helpers;
using UnityEngine;

namespace Systems
{
    public class SpawningBallsSystem:SimpleSystem<SpawningBalls, PositionOnStage, BallsGeneratorComponent>
    {

        protected override void Process(Entity entity, ref SpawningBalls first, ref PositionOnStage second, ref BallsGeneratorComponent third,
            in float deltaTime)
        {
            ref var lastSpawnTime = ref first.LastSpawnTime;
            
            if (Time.time - lastSpawnTime >= 1f)
            {
                AddBall(third.BallPrefab, second.Transform);
                lastSpawnTime = Time.time;
            }
        }
        
        private void AddBall(GameObject ballsBallPrefab, Transform posTransform)
        {
            var ball = Object.Instantiate(ballsBallPrefab);
            ball.transform.position = posTransform.position;
        }
       
    }
}