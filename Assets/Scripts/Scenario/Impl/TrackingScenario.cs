using Player;
using Scenario.Target;
using Scenario.Util;
using UnityEngine;

namespace Scenario.Impl
{
    public class TrackingScenario : Scenario
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private bool useThreeDimensions;

        public override void StartScenario()
        {
            base.StartScenario();
            player.CanFireWeapon(false);
        }

        public override void EndScenario()
        {
            base.EndScenario();
            player.CanFireWeapon(true);
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(targetPrefab, GetSpawnPosition(), Quaternion.identity);
            
            if (useThreeDimensions)
            {
                target.GetComponent<RandomWalkBehaviour>().UseThreeDimensions();
            }
            
            return target;
        }

        private Vector3 GetSpawnPosition()
        {
            var origin = new Vector2(CenterPosition.x, CenterPosition.y);
            var point = StochasticSpawn.InBounds(origin, -MaxX, MaxX, -MaxY, MaxY);
            return new Vector3(point.x, point.y, CenterPosition.z);
        }
    }
}