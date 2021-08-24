using UnityEngine;

namespace Scenario.Target
{
    public class RandomWalkBehaviour : MonoBehaviour
    {
        private const float Thrust = 10f;
        private const float MinDirectionDurationS = 1f;
        private const float MaxDirectionDurationS = 2f;
        private float directionTimer;
        private Rigidbody rb;
        
        public void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
        }

        public void UseThreeDimensions()
        {
            rb.constraints = RigidbodyConstraints.None;
        }
        
        public void FixedUpdate()
        {
            directionTimer -= Time.deltaTime;
 
            if (directionTimer <= 0)
            {
                ChangeTargetDirection();
            }
        }
        
        private void ChangeTargetDirection() // TODO: no ceiling
        {
            rb.velocity = Random.insideUnitSphere.normalized * Thrust;
            directionTimer = Random.Range(MinDirectionDurationS, MaxDirectionDurationS);
        }
    }
}