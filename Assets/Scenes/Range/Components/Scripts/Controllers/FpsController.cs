using Scenes.Range.Components.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Range.Components.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class FpsController : MonoBehaviour
    {
        [SerializeField] private PlayerInput input;
        [Tooltip("The transform component that holds the gun camera."), SerializeField]
        private Transform arms;
        [Tooltip("The position of the arms and gun camera relative to the fps controller GameObject."), SerializeField]
        private Vector3 armPosition;
        [SerializeField, Range(0, 20)] private float aimSensitivity = 1f;
        [SerializeField] private float rotationSmoothness = 0.05f;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private SmoothRotation _rotationX;
        private SmoothRotation _rotationY;
        private float _minVerticalAngle = -90f;
        private float _maxVerticalAngle = 90f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            arms = AssignCharactersCamera();
            _rotationX = new SmoothRotation(RotationX);
            _rotationY = new SmoothRotation(RotationY);
            Cursor.lockState = CursorLockMode.Locked;
            RestrictRotation();
        }
			
        private Transform AssignCharactersCamera()
        {
            var tf = transform;
			arms.SetPositionAndRotation(tf.position, tf.rotation);
			return arms;
        }
        
        private void RestrictRotation()
        {
            _minVerticalAngle = ClampRotationRestriction(_minVerticalAngle, -90, 90);
            _maxVerticalAngle = ClampRotationRestriction(_maxVerticalAngle, -90, 90);
            
            if (_maxVerticalAngle >= _minVerticalAngle)
            {
                return;
            }
            
            Debug.LogWarning("maxVerticalAngle should be greater than minVerticalAngle.");
            var min = _minVerticalAngle;
            _minVerticalAngle = _maxVerticalAngle;
            _maxVerticalAngle = min;
        }

        private static float ClampRotationRestriction(float rotationRestriction, float min, float max)
        {
            if (rotationRestriction >= min && rotationRestriction <= max) return rotationRestriction;
            var message = string.Format("Rotation restrictions should be between {0} and {1} degrees.", min, max);
            Debug.LogWarning(message);
            return Mathf.Clamp(rotationRestriction, min, max);
        }
        
        private void Update()
        {
            arms.position = transform.position + transform.TransformVector(armPosition);
        }
			
        private void FixedUpdate()
        {
            RotateCameraAndCharacter();
        }

        private void RotateCameraAndCharacter()
        {
            var rotationX = _rotationX.Update(RotationX, rotationSmoothness);
            var rotationY = _rotationY.Update(RotationY, rotationSmoothness);
            
            var clampedY = ClampVerticalRotation(rotationY);
            _rotationY.Current = clampedY;
            
			var worldUp = arms.InverseTransformDirection(Vector3.up);
			var rotation = 
                arms.rotation * Quaternion.AngleAxis(rotationX, worldUp) * Quaternion.AngleAxis(clampedY, Vector3.left);
            
            transform.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, 0f);
			arms.rotation = rotation;
        }

        private float RotationX => input.GetRotationDelta().x * aimSensitivity;

        private float RotationY => input.GetRotationDelta().y * aimSensitivity;
        
        private float ClampVerticalRotation(float mouseY)
        {
			var currentAngle = NormalizeAngle(arms.eulerAngles.x);
            var minY = _minVerticalAngle + currentAngle;
            var maxY = _maxVerticalAngle + currentAngle;
            return Mathf.Clamp(mouseY, minY + 0.01f, maxY - 0.01f);
        }
        
        private static float NormalizeAngle(float angleDegrees)
        {
            while (angleDegrees > 180f)
            {
                angleDegrees -= 360f;
            }

            while (angleDegrees <= -180f)
            {
                angleDegrees += 360f;
            }

            return angleDegrees;
        }

        private class SmoothRotation
        {
            private float _current;
            private float _currentVelocity;

            public SmoothRotation(float startAngle)
            {
                _current = startAngle;
            }
				
            /// Returns the smoothed rotation.
            public float Update(float target, float smoothTime)
            {
                return _current = Mathf.SmoothDampAngle(_current, target, ref _currentVelocity, smoothTime);
            }

            public float Current
            {
                set { _current = value; }
            }
        }
    }
}