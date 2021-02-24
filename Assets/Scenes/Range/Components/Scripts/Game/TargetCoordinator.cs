using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class TargetCoordinator : MonoBehaviour
    {
        [SerializeField] private int maxTargets = 5;
        [SerializeField] private float xVariance = 6f;
        [SerializeField] private float yVariance = 4f;
        [SerializeField] private GameObject targetPrefab;
        
        private readonly List<GameObject> _activeTargets = new List<GameObject>();
        private readonly Vector3 _centerPosition = new Vector3(0, 4, 17);

        private void Update()
        {
            RemoveDestroyedTargets(); // TODO: reuse 'destroyed' targets
            TrySpawnTargets();
        }

        private void RemoveDestroyedTargets()
        {
            _activeTargets.RemoveAll(target => target == null);
        }

        private void TrySpawnTargets()
        {
            for (var i = _activeTargets.Count; i < maxTargets; i++)
            {
                var target = Instantiate(targetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
                _activeTargets.Add(target);
            }
        }

        private Vector3 GetRandomSpawnPosition()
        {
            var position = _centerPosition;
            position.x += Random.Range(-xVariance, xVariance);
            position.y += Random.Range(-yVariance, yVariance);
            return position;
        }
    }
}