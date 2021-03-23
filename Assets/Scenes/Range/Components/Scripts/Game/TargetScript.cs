using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class TargetScript : MonoBehaviour
    {
        private static readonly Vector3 PlayerPosition = new Vector3(0, 0, 0);
        [SerializeField] private AudioClip hitSound;
        public bool isHit;

        private void Update()
        {
            if (isHit)
            {
                AudioSource.PlayClipAtPoint(hitSound, PlayerPosition);
                Destroy(gameObject);
            }
        }
    }
}