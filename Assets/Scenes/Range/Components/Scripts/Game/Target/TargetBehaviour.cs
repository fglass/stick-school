using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class TargetBehaviour : MonoBehaviour
    {
        private static readonly Vector3 PlayerPosition = new Vector3(0, 0, 0);
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private GameObject shatteredPrefab;
        [SerializeField] private bool showTrail;

        public bool IsHit { get; set; }
        
        public void Awake()
        {
            GetComponent<TrailRenderer>().enabled = showTrail;
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
            Instantiate(shatteredPrefab, tf.position, tf.rotation);
        }
    }
}