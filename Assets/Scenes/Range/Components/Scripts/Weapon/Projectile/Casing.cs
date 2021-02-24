using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes.Range.Components.Scripts.Projectile
{
	public class Casing : MonoBehaviour {

		[SerializeField] private float despawnTime;
		[SerializeField] private float minimumXForce;
		[SerializeField] private float maximumXForce;
		[SerializeField] private float minimumYForce;
		[SerializeField] private float maximumYForce;
		[SerializeField] private float minimumZForce;
		[SerializeField] private float maximumZForce;
		[SerializeField] private float minimumRotation;
		[SerializeField] private float maximumRotation;
		[SerializeField] private float spinSpeed = 2500.0f;
		[SerializeField] private AudioSource audioSource;
		[SerializeField] private AudioClip[] casingSounds;

		private void Awake () 
		{
			GetComponent<Rigidbody>().AddRelativeTorque (
				Random.Range(minimumRotation, maximumRotation),
				Random.Range(minimumRotation, maximumRotation),
				Random.Range(minimumRotation, maximumRotation) * Time.deltaTime
			);

			GetComponent<Rigidbody>().AddRelativeForce (
				Random.Range (minimumXForce, maximumXForce),
				Random.Range (minimumYForce, maximumYForce),
				Random.Range (minimumZForce, maximumZForce)
			);		     
		}

		private void Start () 
		{
			StartCoroutine(DespawnRoutine());
			StartCoroutine(SoundRoutine());
			transform.rotation = Random.rotation;
		}

		private void FixedUpdate () 
		{
			transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
			transform.Rotate(Vector3.down, spinSpeed * Time.deltaTime);
		}

		private IEnumerator DespawnRoutine() 
		{
			yield return new WaitForSeconds(despawnTime);
			Destroy(gameObject);
		}
		
		private IEnumerator SoundRoutine() 
		{
			yield return new WaitForSeconds(Random.Range(0.25f, 0.85f));
			audioSource.clip = casingSounds[Random.Range(0, casingSounds.Length)];
			audioSource.Play();
		}
	}
}