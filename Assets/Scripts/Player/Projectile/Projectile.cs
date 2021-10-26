using System.Collections;
using Events;
using Scenario.Target;
using UnityEngine;

namespace Player.Projectile
{
	public class Projectile : MonoBehaviour {
		
		private const float DurationS = 5.0f;
		private const string TargetTag = "Target";
		private const string EnvironmentTag = "Environment";
		private const string IgnoreProjectileTag = "Ignore Projectile";

		[SerializeField] private VoidEvent targetHitEvent;
		[SerializeField] private VoidEvent targetMissEvent;
		[SerializeField] private Transform impactPrefab;

		public void Start() 
		{
			StartCoroutine(DespawnRoutine());
		}

		public void OnCollisionEnter(Collision collision) 
		{
			Destroy(gameObject);
			var tf = collision.transform;
			
			if (tf.CompareTag(TargetTag))
			{
				tf.gameObject.GetComponent<TargetController>().IsHit = true;
				targetHitEvent.Raise();
			}
			else if (tf.CompareTag(EnvironmentTag))
			{
				InstantiateImpactPrefab(collision);
				targetMissEvent.Raise();
			} 
			else if (tf.CompareTag(IgnoreProjectileTag))
			{
				targetMissEvent.Raise();
			}
		}
		
		private IEnumerator DespawnRoutine() 
		{
			yield return new WaitForSeconds(DurationS);
			Destroy(gameObject);
		}

		private void InstantiateImpactPrefab(Collision collision)
		{
			var rotation = Quaternion.LookRotation(collision.contacts[0].normal);
			Instantiate(impactPrefab, transform.position, rotation);
		}
	}
}