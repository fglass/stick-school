using Game.Event;
using Scenes.Range.Components.Scripts.Controller.Input;
using Scenes.Range.Components.Scripts.Weapon;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField, Range(0, 20)] private float sensitivity = 0.05f;

        private bool _active;
        private WeaponBehaviour _weapon;
        private Rigidbody _rigidbody;
        private AudioSource _audioSource;
        private float _verticalCameraAngle;

        public void OnEnable()
        {
            _weapon = transform.Find("WeaponRoot").Find("Weapon").GetComponent<WeaponBehaviour>();
            EventBus.OnPlay += SetActive;
            EventBus.OnResume += SetActive;
            EventBus.OnPause += SetInactive;
        }

        public void OnDisable()
        {
            EventBus.OnPlay -= SetActive;
            EventBus.OnResume -= SetActive;
            EventBus.OnPause -= SetInactive;
        }

        private void SetActive()
        {
            _active = true;
            _weapon.enabled = true;
        }

        private void SetInactive()
        {
            _active = false;
            _weapon.enabled = false;
        }

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            
            _audioSource = GetComponent<AudioSource>();
            _audioSource.loop = true;
            
            cameraTransform = InitCamera();
        }
			
        private Transform InitCamera()
        {
            var tf = transform;
			cameraTransform.SetPositionAndRotation(tf.position, tf.rotation);
			return cameraTransform;
        }

        private void Update()
        {
            if (_active)
            {
                Rotate();
            }
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

        private float RotationDeltaX => InputManager.GetRotationDelta().x * InputMultiplier * sensitivity;

        private float RotationDeltaY => -InputManager.GetRotationDelta().y * InputMultiplier * sensitivity;

        private float InputMultiplier => InputManager.IsUsingController() ? 3f : 1f;
    }
}