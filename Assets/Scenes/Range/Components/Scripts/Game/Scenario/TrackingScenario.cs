using Scenes.Range.Components.Scripts.Game.Target;
using Scenes.Range.Components.Scripts.Game.Util;
using Scenes.Range.Components.Scripts.Weapon;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class TrackingScenario : TrainingScenario
    {
        [SerializeField] private GameObject weapon;
        [SerializeField] private bool useThreeDimensions;
        
        public override void StartScenario()
        {
            MaxTargets = 1;
            weapon.GetComponent<WeaponScript>().CanFire = false;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetSpawnPosition(), Quaternion.identity);

            target.AddComponent<HoverBehaviour>();
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