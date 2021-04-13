using Scenes.Range.Components.Scripts.Game.Util;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class StochasticScenario : TrainingScenario
    {
        public override void StartScenario()
        {
            MaxTargets = 5;
        }

        protected override GameObject SpawnTarget()
        {
            var origin = new Vector2(CenterPosition.x, CenterPosition.y);
            var point = StochasticSpawn.InBounds(origin, -MaxX, MaxX, -MaxY, MaxY);
            var position = new Vector3(point.x, point.y, CenterPosition.z);
            
            var target = Instantiate(TargetPrefab, position, Quaternion.identity);
            FreezeTarget(target);
            return target;
        }

        private static void FreezeTarget(GameObject target)
        {
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}