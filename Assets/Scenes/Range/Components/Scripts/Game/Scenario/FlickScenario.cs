using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class FlickScenario : TrainingScenario
    {
        private const float MinDuration = 0.5f;
        private const float MaxDuration = 2f;
        private bool _spawnInCenter = true;

        public void Start()
        {
            MaxTargets = 1;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetSpawnPosition(), Quaternion.identity);

            if (target.transform.position != CenterPosition)
            {
                StartCoroutine(DespawnRoutine(target));
            }
            
            return target;
        }

        private Vector3 GetSpawnPosition()
        {
            if (_spawnInCenter)
            {
                _spawnInCenter = false;
                return CenterPosition;
            }
            
            _spawnInCenter = true;
            return GetRandomSpawnPosition();
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var position = CenterPosition;
            position.x += Random.Range(-MaxX, MaxX) + 1;
            position.y += Random.Range(-MaxY, MaxY) + 1;
            return position;
        }
        
        private static IEnumerator DespawnRoutine(Object target) {
            var duration = Random.Range(MinDuration, MaxDuration);
            yield return new WaitForSeconds(duration);
            Destroy(target);
        }
    }
}