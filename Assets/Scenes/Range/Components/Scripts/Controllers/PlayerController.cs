using Scenes.Range.Components.Scripts.Controllers.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Range.Components.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private PlayerInput input;
        [SerializeField, Range(0, 20)] private float sensitivity = 0.05f;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private float _verticalCameraAngle;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            
            cameraTransform = InitCamera();
            Cursor.lockState = CursorLockMode.Locked;
        }
			
        private Transform InitCamera()
        {
            var tf = transform;
			cameraTransform.SetPositionAndRotation(tf.position, tf.rotation);
			return cameraTransform;
        }

        private void Update()
        {
            Rotate();
        }
        
        private void Rotate()
        {
            DoHorizontalRotation();
            DoVerticalRotation();
        }

        private void DoHorizontalRotation()
        {
            transform.Rotate(new Vector3(0f, RotationDeltaX, 0f), Space.Self);
        }

        private void DoVerticalRotation()
        {
            _verticalCameraAngle += RotationDeltaY;
            _verticalCameraAngle = Mathf.Clamp(_verticalCameraAngle, -89f, 89f);
            cameraTransform.localEulerAngles = new Vector3(_verticalCameraAngle, 0, 0);
        }

        private float RotationDeltaX => input.GetRotationDelta().x * InputMultiplier * sensitivity;

        private float RotationDeltaY => -input.GetRotationDelta().y * InputMultiplier * sensitivity;

        private float InputMultiplier => input.IsUsingController() ? 3f : 1f;
    }
}