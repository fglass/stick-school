using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public abstract class TrainingScenario : MonoBehaviour
    {
        private readonly List<GameObject> _activeTargets = new List<GameObject>();
        protected readonly Vector3 CenterPosition = new Vector3(0f, 4.75f, 17f);
        protected float MaxX = 6.75f;
        protected float MaxY = 5f;
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
    }
}