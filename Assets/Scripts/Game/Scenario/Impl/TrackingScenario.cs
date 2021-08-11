using Scenes.Range.Components.Scripts.Game.Target;
using Scenes.Range.Components.Scripts.Game.Util;
using Scenes.Range.Components.Scripts.Weapon;
using UnityEngine;

namespace Game.Scenario.Impl
{
    public class TrackingScenario : Scenario
    {
        [SerializeField] private GameObject weapon;
        [SerializeField] private bool useThreeDimensions;
        
        public override void StartScenario()
        {
            Name = "Tracking Scenario";
            MaxTargets = 1;
            weapon.GetComponent<WeaponBehaviour>().CanFire = false;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetSpawnPosition(), Quaternion.identity);

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