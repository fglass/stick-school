using System.Collections;
using Scenes.Range.Components.Scripts.Controllers.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Range.Components.Scripts.Weapon
{
    public class WeaponScript : MonoBehaviour
    {
        [SerializeField] private float lightDuration = 0.02f;
        [SerializeField] private float fovSpeed = 15.0f;
        [SerializeField] private float defaultFov = 40.0f;
        [SerializeField] private float aimFov = 15.0f;
        [SerializeField] private float bulletForce = 400;

        [SerializeField] private PlayerInput input;
        [SerializeField] private Camera gunCamera;
        [SerializeField] private Light muzzleFlash;
        [SerializeField] private ParticleSystem muzzleParticles;
        
        [SerializeField] private AudioSource mainAudioSource;
        [SerializeField] private AudioSource shootAudioSource;
        [SerializeField] private AudioClip aimSound;
        [SerializeField] private AudioClip shootSound;

        [SerializeField] private Transform bulletPrefab;
        [SerializeField] private Transform casingPrefab;
        [SerializeField] private Transform casingSpawnPoint;
        [SerializeField] private Transform bulletSpawnPoint;

        private Animator _animator;
        private static readonly int Aim = Animator.StringToHash("Aim");

        private bool _isAds;
        private bool _hasSoundPlayed;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            muzzleFlash.enabled = false;
            mainAudioSource.clip = aimSound;
            shootAudioSource.clip = shootSound;
        }

        private void Update()
        {
            if (input.IsAds()) 
            {
                AimDownSight();
            }
            else
            {
                ReleaseAim();
            }
            
            if (input.IsFiring())
            {
                Fire();
            }
        }

        private void AimDownSight()
        {
            gunCamera.fieldOfView = Mathf.Lerp (gunCamera.fieldOfView, aimFov, fovSpeed * Time.deltaTime);
            _animator.SetBool(Aim, true);
            _isAds = true;

            if (!_hasSoundPlayed) 
            {
                mainAudioSource.Play();
                _hasSoundPlayed = true;
            }
        }
        
        private void ReleaseAim()
        {
            gunCamera.fieldOfView = Mathf.Lerp(gunCamera.fieldOfView, defaultFov,fovSpeed * Time.deltaTime);
            _animator.SetBool(Aim, false);
            _isAds = false;
            _hasSoundPlayed = false;
        }

        private void Fire()
        {
            _animator.Play("Fire", 0, 0f);
            shootAudioSource.Play();
            muzzleParticles.Emit(1);

            DoMuzzleFlash();
            SpawnProjectile();
        }
        
        private void DoMuzzleFlash()
        {
            StartCoroutine(MuzzleFlashLightRoutine());
            _animator.Play(_isAds ? "Aim Fire" : "Fire", 0, 0f);
            muzzleParticles.Emit (1);
        }
        
        private IEnumerator MuzzleFlashLightRoutine() 
        {
            muzzleFlash.enabled = true;
            yield return new WaitForSeconds(lightDuration);
            muzzleFlash.enabled = false;
        }

        private void SpawnProjectile()
        {
            var bulletTransform = bulletSpawnPoint.transform;
            var bullet = Instantiate(bulletPrefab, bulletTransform.position, bulletTransform.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletForce;

            var casingTransform = casingSpawnPoint.transform;
            Instantiate(casingPrefab, casingTransform.position, casingTransform.rotation);
        }
    }
}