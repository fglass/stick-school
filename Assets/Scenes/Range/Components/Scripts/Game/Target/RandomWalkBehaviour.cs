using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class RandomWalkBehaviour : MonoBehaviour
    {
        private const float Thrust = 10f;
        private const float MinDirectionDurationS = 1f;
        private const float MaxDirectionDurationS = 2f;
        private Rigidbody _rigidbody;
        private float _directionTimer;
        
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
            _rigidbody.velocity = Random.insideUnitSphere.normalized * Thrust;
            _directionTimer = Random.Range(MinDirectionDurationS, MaxDirectionDurationS);
        }
    }
}