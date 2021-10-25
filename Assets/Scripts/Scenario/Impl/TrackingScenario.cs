using Player;
using Scenario.Target;
using Scenario.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenario.Impl
{
    public class TrackingScenario : Scenario
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private bool useThreeDimensions;

        public override void StartScenario()
        {
            base.StartScenario();
            player.CanFire(false);
        }

        public override void EndScenario()
        {
            base.EndScenario();
            player.CanFire(true);
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(targetPrefab, GetSpawnPosition(), Quaternion.identity);
            
            target.AddComponent<HealthBehaviour>();
            var walkBehaviour = target.AddComponent<RandomWalkBehaviour>();
            
            if (useThreeDimensions)
            {
                walkBehaviour.UseThreeDimensions();
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