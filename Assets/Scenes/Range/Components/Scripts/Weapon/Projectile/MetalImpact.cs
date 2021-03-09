using System.Collections;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Weapon.Projectile
{
	public class MetalImpact : MonoBehaviour {

		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip[] impactSounds;
		private const float DurationS = 8.0f;

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
