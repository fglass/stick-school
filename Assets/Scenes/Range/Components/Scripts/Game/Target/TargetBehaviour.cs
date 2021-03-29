using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class TargetBehaviour : MonoBehaviour
    {
        private static readonly Vector3 PlayerPosition = new Vector3(0, 0, 0);
        
        [SerializeField] private AudioClip hitSound;
        [SerializeField] private GameObject shatteredPrefab;
        public bool isHit;

        private void Update()
        {
            if (isHit)
            {
                AudioSource.PlayClipAtPoint(hitSound, PlayerPosition);
                SpawnShatteredObject();
                Destroy(gameObject);
            }
        }

        private void SpawnShatteredObject()
        {
            var tf = transform;
            Instantiate(shatteredPrefab, tf.position, tf.rotation);
        }
    }
}