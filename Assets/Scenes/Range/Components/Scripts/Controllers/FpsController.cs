using Scenes.Range.Components.Scripts.Controllers.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Range.Components.Scripts.Controllers
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(AudioSource))]
    public class FpsController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private PlayerInput input;
        [SerializeField, Range(0, 20)] private float aimSensitivity = 1f;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            
            cameraTransform = AssignPlayerCamera();
            Cursor.lockState = CursorLockMode.Locked;
        }
			
        private Transform AssignPlayerCamera()
        {
            var tf = transform;
			cameraTransform.SetPositionAndRotation(tf.position, tf.rotation);
			return cameraTransform;
        }

        private void FixedUpdate()
        {
            RotatePlayer();
        }

        private void RotatePlayer()
        {
			var worldUp = cameraTransform.InverseTransformDirection(Vector3.up);
			var rotation = cameraTransform.rotation * 
                                     Quaternion.AngleAxis(RotationX, worldUp) * 
                                     Quaternion.AngleAxis(RotationY, Vector3.left);
            
            transform.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, 0f);
			cameraTransform.rotation = rotation;
            // cameraTransform.Rotate(Vector3.up * RotationX);
            // cameraTransform.Rotate(Vector3.left * RotationY);
        }

        private float RotationX => input.GetRotationDelta().x * aimSensitivity;

        private float RotationY => input.GetRotationDelta().y * aimSensitivity;
    }
}