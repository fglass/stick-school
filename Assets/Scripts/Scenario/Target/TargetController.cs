using UnityEngine;

namespace Scenario.Target
{
    public class TargetController : MonoBehaviour
    {
        private static readonly Vector3 PlayerPosition = new Vector3(0, 0, 0);
        private static readonly int ColourId = Shader.PropertyToID("TargetColour");
        
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private GameObject shatteredPrefab;
        
        private Material _material;
        private Color _initialColour;

        public bool IsHit { get; set; }
        
        public void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
            _initialColour = _material.GetColor(ColourId);
        }
        
        public void Update()
        {
            if (IsHit)
            {
                OnHit();
            }
        }

        private void OnHit()
        {
            AudioSource.PlayClipAtPoint(hitSound, PlayerPosition);
            SpawnShatteredObject();
            Destroy(gameObject);
        }

        private void SpawnShatteredObject()
        {
            var tf = transform;
            var shatteredObject = Instantiate(shatteredPrefab, tf.position, tf.rotation);
            TryUpdateColour(shatteredObject);
        }

        private void TryUpdateColour(GameObject shatteredObject)
        {            
            var targetColour = _material.GetColor(ColourId);
            if (targetColour != _initialColour)
            {
                shatteredObject.GetComponent<ShatterEffect>().SetColour(targetColour);
            }
        }
    }
}