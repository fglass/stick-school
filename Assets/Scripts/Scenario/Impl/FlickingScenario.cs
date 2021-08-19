// ReSharper disable HeuristicUnreachableCode
// ReSharper disable ConditionIsAlwaysTrueOrFalse
using System.Collections;
using Scenes.Range.Components.Scripts.Game.Util;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Scenario.Impl
{
    #pragma warning disable 162
    public class FlickingScenario : global::Scenario.Scenario
    {
        private const bool DebugMode = false;
        private const float MinDuration = 0.5f;
        private const float MaxDuration = 1.5f;
        private bool _spawnInCenter = true;

        public void Awake()
        {
            Name = "Flicking";
            MaxTargets = 1;
        }

        public override void StartScenario()
        {
            _spawnInCenter = true;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetSpawnPosition(), Quaternion.identity);
            
            if (DebugMode)
            {
                target.GetComponent<MeshCollider>().enabled = false;
            } else if (target.transform.position != CenterPosition)
            {
                StartCoroutine(DespawnRoutine(target));
            }

            return target;
        }

        public override void FixedUpdateScenario()
        {
            if (DebugMode)
            {
                SpawnTarget();
            }
        }

        private Vector3 GetSpawnPosition()
        {
            var position = _spawnInCenter ? CenterPosition : GetRandomSpawnPosition();
            _spawnInCenter = !_spawnInCenter;
            return position;
        }
        
        private Vector3 GetRandomSpawnPosition()
        {
            var origin = new Vector2(CenterPosition.x, CenterPosition.y);
            var point = StochasticSpawn.InHollowRectangle(origin, MaxX, MaxY, 1f);
            return new Vector3(point.x, point.y, CenterPosition.z);
        }
        
        private static IEnumerator DespawnRoutine(Object target) {
            var duration = Random.Range(MinDuration, MaxDuration);
            yield return new WaitForSeconds(duration);
            Destroy(target);
        }
    }
}