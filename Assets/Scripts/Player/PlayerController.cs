using Input;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Range(0, 20)] private float sensitivity = 0.05f;
        [SerializeField] private Transform mainCameraTransform;
        [SerializeField] private WeaponController weapon;

        private Vector3 _currentEulerAngles; // Never rely on reading Quaternion.eulerAngles to increment rotation
        
        public void Awake()
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        public void Update()
        {
            Rotate();
        }
        
        public void Reset()
        {
            CanFireWeapon(false);
            ResetCameras();
        }
        
        public void CanFireWeapon(bool canFire)
        {
            weapon.CanFire = canFire;
        }
        
        private void ResetCameras()
        {
            transform.rotation = Quaternion.identity;
            mainCameraTransform.rotation = Quaternion.identity;
        }

        private void Rotate()
        {
            var inputDelta = GetInputRotationDelta();
            var rotationDelta = new Vector3(-inputDelta.y, inputDelta.x, 0);

            _currentEulerAngles += rotationDelta;
            _currentEulerAngles.x = Mathf.Clamp(_currentEulerAngles.x, -90, 90);
                
            mainCameraTransform.eulerAngles = _currentEulerAngles;
            transform.eulerAngles = _currentEulerAngles;
        }
        
        private Vector2 GetInputRotationDelta() => InputManager.GetRotationDelta() * (GetInputMultiplier() * sensitivity);

        private static float GetInputMultiplier() => InputManager.IsUsingController ? 10f : 1f;
    }
}