using System.Collections;
using Events;
using Scenario.Target;
using UnityEngine;

namespace Weapon.Projectile
{
	public class Projectile : MonoBehaviour {

		[SerializeField] private VoidEvent targetHitEvent;
		[SerializeField] private VoidEvent targetMissEvent;
		[SerializeField] private Transform metalImpactPrefab;
		private const float DurationS = 5.0f;
		private const string TargetTag = "Target";
		private const string MetalTag = "Metal";

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
				tf.gameObject.GetComponent<TargetBehaviour>().IsHit = true;
				targetHitEvent.Raise();
			}
			else if (tf.CompareTag(MetalTag)) 
			{
				InstantiateImpactPrefab(collision);
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
			Instantiate(
				metalImpactPrefab, 
				transform.position, 
				Quaternion.LookRotation(collision.contacts[0].normal)
			);
		}
	}
}