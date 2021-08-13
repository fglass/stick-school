using Controller.Input;
using UnityEngine;

namespace Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField, Range(0, 20)] private float sensitivity = 0.05f;

        public void Start()
        {
            InitCamera();
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
			
        private void InitCamera()
        {
            var tf = transform;
			cameraTransform.SetPositionAndRotation(tf.position, tf.rotation);
        }

        private void Update()
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

        private static float GetInputMultiplier() => InputManager.IsUsingController() ? 3f : 1f;
    }
}