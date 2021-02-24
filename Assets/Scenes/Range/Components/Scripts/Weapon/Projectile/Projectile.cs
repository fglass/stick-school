using System.Collections;
using Scenes.Range.Components.Scripts.Target;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Projectile
{
	public class Projectile : MonoBehaviour {

		[SerializeField] private Transform metalImpactPrefab;
		private const float DurationS = 8.0f;
		private const string MetalTag = "Metal";
		private const string TargetTag = "StaticTarget";		

		private void Start() 
		{
			StartCoroutine(DespawnRoutine());
		}

		private void OnCollisionEnter(Collision collision) 
		{
			Destroy(gameObject);

			var tf = collision.transform;

			if (tf.CompareTag(MetalTag)) 
			{
				InstantiateImpactPrefab(collision);
			}
			else if (tf.CompareTag(TargetTag)) 
			{
				tf.gameObject.GetComponent<StaticTargetScript>().isHit = true;
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