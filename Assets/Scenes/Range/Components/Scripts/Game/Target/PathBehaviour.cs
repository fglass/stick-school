using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class PathBehaviour : MonoBehaviour
    {
        private const float Thrust = 10f;
        private const float MinDirectionDurationS = 1f;
        private const float MaxDirectionDurationS = 2f;
        private Rigidbody _rigidbody;
        private float _directionTimer;
        private const string MetalTag = "Metal";

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        }
        
        public void FixedUpdate()
        {
            _directionTimer -= Time.deltaTime;
 
            if (_directionTimer <= 0)
            {
                ChangeTargetDirection();
            }
        }
        
        private void ChangeTargetDirection() { 
            var direction = Random.insideUnitCircle;
            transform.rotation = new Quaternion(direction.x, direction.y, 0f, 1f);
            _rigidbody.velocity = transform.up * Thrust;
            _directionTimer = Random.Range(MinDirectionDurationS, MaxDirectionDurationS);
        }
        
        public void OnCollisionEnter(Collision collision) 
        {
            var tf = collision.transform;

            if (tf.CompareTag(MetalTag)) 
            {
                OnBounds();
            }
        }

        private void OnBounds()
        {
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.right);
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            _rigidbody.velocity = transform.up * Thrust;
        }
    }
}