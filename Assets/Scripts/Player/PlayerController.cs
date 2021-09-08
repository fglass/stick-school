using Input;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField, Range(0, 20)] private float sensitivity = 0.05f;

        public void Awake()
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
        
        public void ResetCameras()
        {
            transform.rotation = Quaternion.identity;
            cameraTransform.rotation = Quaternion.identity;
        }

        public void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            var inputDelta = GetInputRotationDelta();
            var rotationDelta = new Vector3(-inputDelta.y, inputDelta.x, 0);
            cameraTransform.eulerAngles += rotationDelta; // TODO: clamp x axis
            transform.eulerAngles += rotationDelta;
        }
        
        private Vector2 GetInputRotationDelta() => InputManager.GetRotationDelta() * (GetInputMultiplier() * sensitivity);

        private static float GetInputMultiplier() => InputManager.IsUsingController ? 3.5f : 1f;
    }
}