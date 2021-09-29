using System.Collections.Generic;
using UnityEngine;

namespace Scenario
{
    public abstract class Scenario : MonoBehaviour
    {
        protected const float MaxX = 12f;
        protected const float MaxY = 5f;

        [SerializeField] private string scenarioName;
        [SerializeField] private int maxTargets = 1;
        [SerializeField] protected GameObject targetPrefab;
        protected readonly Vector3 CenterPosition = new Vector3(0f, 4.75f, 17f);
        private readonly List<GameObject> _activeTargets = new List<GameObject>();

        public string Name => scenarioName;

        public virtual void StartScenario()
        {
            
        }
        
        public void UpdateScenario()
        {
            RemoveDestroyedTargets();
            TrySpawnTargets();
        }

        public virtual void FixedUpdateScenario()
        {
            
        }

        public virtual void EndScenario()
        {
            _activeTargets.ForEach(Destroy);
        }

        private void RemoveDestroyedTargets()
        {
            _activeTargets.RemoveAll(target => target == null);
        }

        private void TrySpawnTargets()
        {
            for (var i = _activeTargets.Count; i < maxTargets; i++)
            {
                var target = SpawnTarget();
                _activeTargets.Add(target);
            }
        }

        protected abstract GameObject SpawnTarget();
    }
}