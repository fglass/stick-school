using UnityEngine;

namespace Environment
{
    public class WireframeController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 1f;
        
        public void Update()
        {
            transform.Rotate(0, 0, Time.deltaTime * rotationSpeed, Space.World);
        }
    }
}
