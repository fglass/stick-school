using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class StochasticScenario : TrainingScenario
    {
        public void Start()
        {
            MaxTargets = 5;
        }

        protected override GameObject SpawnTarget()
        {
            return Instantiate(TargetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var position = CenterPosition;
            position.x += Random.Range(-MaxX, MaxX) + 1;
            position.y += Random.Range(-MaxY, MaxY) + 1;
            return position;
        }
    }
}