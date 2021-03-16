using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class TrackingScenario : TrainingScenario
    {
        private const float VelocityMultiplier = 5f;
        private const float MinDirectionDurationS = 1f;
        private const float MaxDirectionDurationS = 2f;

        private Rigidbody _rigidbody;
        private bool _isSpawned;
        private float _directionTimer;
        
        public void Start()
        {
            MaxTargets = 1;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetRandomSpawnPosition(), Quaternion.identity);
            _rigidbody = target.GetComponent<Rigidbody>();
            _isSpawned = true;
            return target;
        }

        public override void FixedUpdateScenario()
        {
            if (!_isSpawned)
            {
                return;
            }
            
            _directionTimer -= Time.deltaTime;
 
            if (_directionTimer <= 0) {
                ChangeTargetDirection();
            }
 
            _rigidbody.velocity = transform.up * VelocityMultiplier;
        }
        
        private void ChangeTargetDirection() { 
            // TODO: bounds check
            var angle = Random.Range(0f, 360f);
            var quat = Quaternion.AngleAxis(angle, Vector3.forward);
            
            var newUp = quat * Vector3.up;
            newUp.z = 0;
            newUp.Normalize();
            
            transform.up = newUp;
            _directionTimer = Random.Range(MinDirectionDurationS, MaxDirectionDurationS);
        }
    }
}