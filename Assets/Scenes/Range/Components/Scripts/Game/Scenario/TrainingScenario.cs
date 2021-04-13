using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public abstract class TrainingScenario : MonoBehaviour
    {
        protected const float MaxX = 6.75f;
        protected const float MaxY = 5f;
        protected int MaxTargets = 5;
        protected readonly Vector3 CenterPosition = new Vector3(0f, 4.75f, 17f);
        private readonly List<GameObject> _activeTargets = new List<GameObject>();

        public GameObject TargetPrefab { get; set; }

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

        private void RemoveDestroyedTargets()
        {
            _activeTargets.RemoveAll(target => target == null);
        }

        private void TrySpawnTargets()
        {
            for (var i = _activeTargets.Count; i < MaxTargets; i++)
            {
                var target = SpawnTarget();
                _activeTargets.Add(target);
            }
        }

        protected abstract GameObject SpawnTarget();
    }
}