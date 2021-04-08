using Scenes.Range.Components.Scripts.Game.Target;
using Scenes.Range.Components.Scripts.Game.Util;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class TrackingScenario : TrainingScenario
    {
        [SerializeField] private bool useThreeDimensions;
        
        public void Start()
        {
            MaxTargets = 1;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetSpawnPosition(), Quaternion.identity);
            var behaviour = target.AddComponent<RandomWalkBehaviour>();
            if (useThreeDimensions)
            {
                behaviour.UseThreeDimensions();
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