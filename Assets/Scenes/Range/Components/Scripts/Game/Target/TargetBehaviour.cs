using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class TargetBehaviour : MonoBehaviour
    {
        private static readonly Vector3 PlayerPosition = new Vector3(0, 0, 0);
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private GameObject shatteredPrefab;
        [SerializeField] private bool showTrail;
        private Material _material;
        private Color _initialColour;

        public bool IsHit { get; set; }
        
        public void Awake()
        {
            GetComponent<TrailRenderer>().enabled = showTrail;
            _material = GetComponent<MeshRenderer>().material;
            _initialColour = _material.color;
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
            var targetColour = _material.color;
            if (targetColour != _initialColour)
            {
                shatteredObject.GetComponent<ShatterEffect>().SetColour(targetColour);
            }
        }
    }
}