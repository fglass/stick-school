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
    }
}