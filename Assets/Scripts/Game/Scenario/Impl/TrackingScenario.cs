using Game.Target;
using Scenes.Range.Components.Scripts.Game.Target;
using Scenes.Range.Components.Scripts.Game.Util;
using UnityEngine;
using Weapon;

namespace Game.Scenario.Impl
{
    public class TrackingScenario : Scenario
    {
        [SerializeField] private GameObject weapon;
        [SerializeField] private bool useThreeDimensions;
        
        public void Awake()
        {
            Name = "Tracking";
            MaxTargets = 1;
        }

        public override void StartScenario()
        {
            base.StartScenario();
            weapon.GetComponent<WeaponBehaviour>().CanFire = false;
        }

        public override void EndScenario()
        {
            base.EndScenario();
            weapon.GetComponent<WeaponBehaviour>().CanFire = true;
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