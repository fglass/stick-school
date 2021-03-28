using Scenes.Range.Components.Scripts.Game.Util;
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
            var origin = new Vector2(CenterPosition.x, CenterPosition.y);
            var point = StochasticSpawn.InBounds(origin, -MaxX, MaxX, -MaxY, MaxY);
            var position = new Vector3(point.x, point.y, CenterPosition.z);
            return Instantiate(TargetPrefab, position, Quaternion.identity);
        }
    }
}