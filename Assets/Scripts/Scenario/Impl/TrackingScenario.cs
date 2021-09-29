using Player;
using Scenario.Target;
using Scenario.Util;
using UnityEngine;

namespace Scenario.Impl
{
    public class TrackingScenario : Scenario
    {
        [SerializeField] private GameObject weapon;
        [SerializeField] private bool useThreeDimensions;
        
        public void Awake()
        {
            Name = "Tracking";
        }

        public override void StartScenario()
        {
            base.StartScenario();
            weapon.GetComponent<WeaponController>().CanFire = false;
        }

        public override void EndScenario()
        {
            base.EndScenario();
            weapon.GetComponent<WeaponController>().CanFire = true;
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