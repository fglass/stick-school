using UnityEngine;

namespace Scenario.Target
{
    public class RandomWalkBehaviour : MonoBehaviour
    {
        private const float Thrust = 10f;
        private const float MinDirectionDurationS = 1f;
        private const float MaxDirectionDurationS = 2f;
        private float _directionTimer;
        private Rigidbody _rigidbody;
        
        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }

        public void UseThreeDimensions()
        {
            _rigidbody.constraints = RigidbodyConstraints.None;
        }
        
        public void FixedUpdate()
        {
            _directionTimer -= Time.deltaTime;
 
            if (_directionTimer <= 0)
            {
                ChangeTargetDirection();
            }
        }
        
        private void ChangeTargetDirection()
        {
            var direction = Random.insideUnitSphere.normalized;
            _rigidbody.velocity = direction * Thrust;
            _directionTimer = Random.Range(MinDirectionDurationS, MaxDirectionDurationS);
        }
    }
}