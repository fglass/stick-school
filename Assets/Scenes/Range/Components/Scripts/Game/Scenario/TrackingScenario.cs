using Scenes.Range.Components.Scripts.Game.Util;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class TrackingScenario : TrainingScenario
    {
        private const float VelocityMultiplier = 5f;
        private const float MinDirectionDurationS = 1f;
        private const float MaxDirectionDurationS = 2f;

        private Rigidbody _rigidbody;
        private float _directionTimer;
        
        public void Start()
        {
            MaxTargets = 1;
        }

        protected override GameObject SpawnTarget()
        {
            var target = Instantiate(TargetPrefab, GetSpawnPosition(), Quaternion.identity);
            _rigidbody = target.GetComponent<Rigidbody>();
            return target;
        }

        private Vector3 GetSpawnPosition()
        {
            var origin = new Vector2(CenterPosition.x, CenterPosition.y);
            var point = StochasticSpawn.InBounds(origin, -MaxX, MaxX, -MaxY, MaxY);
            return new Vector3(point.x, point.y, CenterPosition.z);
        }

        public override void FixedUpdateScenario()
        {
            if (_rigidbody == null)
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