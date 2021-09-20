using System.Collections;
using UnityEngine;

namespace Player.Projectile
{
	public class ProjectileImpact : MonoBehaviour {

		private const float DurationS = 8.0f;
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip[] impactSounds;

		private void Start () {
			StartCoroutine(DespawnRoutine());
			audioSource.clip = impactSounds[Random.Range(0, impactSounds.Length)];
			audioSource.Play();
		}
	
		private IEnumerator DespawnRoutine() {
			yield return new WaitForSeconds(DurationS);
			Destroy(gameObject);
		}
	}
}
