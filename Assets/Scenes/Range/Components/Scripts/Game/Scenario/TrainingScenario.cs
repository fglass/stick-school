using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public abstract class TrainingScenario : MonoBehaviour
    {
        private readonly List<GameObject> _activeTargets = new List<GameObject>();
        protected readonly Vector3 CenterPosition = new Vector3(0, 4, 17);
        protected float MaxX = 6f;
        protected float MaxY = 4f;
        protected int MaxTargets = 5;
        
        public GameObject TargetPrefab { get; set; }

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
        
        protected Vector3 GetRandomSpawnPosition()
        {
            var position = CenterPosition;
            position.x += Random.Range(-MaxX, MaxX) + 1;
            position.y += Random.Range(-MaxY, MaxY) + 1;
            return position;
        }
    }
}