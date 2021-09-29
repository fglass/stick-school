using System.Collections;
using Scenario.Util;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Scenario.Impl
{
    public class FlickingScenario : Scenario
    {
        private const float MinDuration = 0.5f;
        private const float MaxDuration = 1.5f;
        
        [SerializeField] private bool debugMode;
        private bool _spawnInCenter = true;

        public void Awake()
        {
            Name = "Flicking";
        }

        public override void StartScenario()
        {
            base.StartScenario();
            _spawnInCenter = true;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(targetPrefab, GetSpawnPosition(), Quaternion.identity);
            
            if (debugMode)
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
            base.FixedUpdateScenario();
            if (debugMode)
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