using Scenario.Util;
using UnityEngine;

namespace Scenario.Impl
{
    public class StochasticScenario : Scenario
    {
        protected override GameObject SpawnTarget()
        {
            var origin = new Vector2(CenterPosition.x, CenterPosition.y);
            var randomPoint = StochasticSpawn.InBounds(origin, -MaxX, MaxX, -MaxY, MaxY);
            var position = new Vector3(randomPoint.x, randomPoint.y, CenterPosition.z);
            
            var target = Instantiate(targetPrefab, position, Quaternion.identity);
            target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            
            return target;
        }
    }
}