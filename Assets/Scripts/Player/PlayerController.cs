using Input;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Range(0, 20)] private float sensitivity = 0.05f;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private WeaponController weapon;

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
            CanFire(false);
            ResetCameras();
        }
        
        public void CanFire(bool canFire)
        {
            weapon.CanFire = canFire;
        }
        
        private void ResetCameras()
        {
            transform.rotation = Quaternion.identity;
            cameraTransform.rotation = Quaternion.identity;
        }

        private void Rotate()
        {
            var inputDelta = GetInputRotationDelta();
            var rotationDelta = new Vector3(-inputDelta.y, inputDelta.x, 0);
            cameraTransform.eulerAngles += rotationDelta; // TODO: clamp x axis
            transform.eulerAngles += rotationDelta;
        }
        
        private Vector2 GetInputRotationDelta() => InputManager.GetRotationDelta() * (GetInputMultiplier() * sensitivity);

        private static float GetInputMultiplier() => InputManager.IsUsingController ? 10f : 1f;
    }
}