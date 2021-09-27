using Input;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InputDebug : MonoBehaviour
    {
        private TextMeshProUGUI console;
        private Transform mainCamera;

        public void Awake()
        {
            console = GetComponent<TextMeshProUGUI>();
            mainCamera = Camera.main.transform;
        }

        public void Update()
        {
            var rotationDelta = InputManager.GetRotationDelta();
            var cameraDelta = Mathf.Floor(mainCamera.rotation.eulerAngles.y);
            
            console.text = $"Horizontal: {rotationDelta.x:0.###}\n" +
                           $"Vertical: {rotationDelta.y:0.###}\n" +
                           $"Rotation: {cameraDelta:0}\u00B0";
        }
    }
}