using System;
using System.Collections;
using Events;
using Input;
using Scenario.Target;
using UnityEngine;

namespace Player
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private float lightDuration = 0.02f;
        [SerializeField] private float fovSpeed = 15.0f;
        [SerializeField] private float defaultFov = 40.0f;
        [SerializeField] private float aimFov = 15.0f;
        [SerializeField] private float projectileSpeed = 400;

        [SerializeField] private BoolEvent toggleCrosshairEvent;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera weaponCamera;
        [SerializeField] private Light muzzleFlash;
        [SerializeField] private ParticleSystem muzzleParticleSystem;
        
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject casingPrefab;
        [SerializeField] private Transform weaponBarrel;
        [SerializeField] private Transform casingSpawn;

        [SerializeField] private AudioSource mainAudioSource;
        [SerializeField] private AudioSource shootAudioSource;

        private const int TargetLayerMask = 1 << 9;
        private const string AimFireAnimationId = "Aim Fire";
        private const string FireAnimationId = "Fire";
        private static readonly int AimAnimatorState = Animator.StringToHash("Aim");

        private Animator _animator;
        private bool _isAds;
        private bool _hasSoundPlayed;

        public bool CanFire { get; set; } = true;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            muzzleFlash.enabled = false;
        }

        public void Update()
        {
            if (InputManager.IsAds()) 
            {
                AimDownSight();
            }
            else
            {
                ReleaseAim();
            }
            
            if (CanFire && InputManager.IsFiring())
            {
                Fire();
            }
            
            CheckIfTargetHovered();
        }

        private void AimDownSight()
        {
            weaponCamera.fieldOfView = Mathf.Lerp (weaponCamera.fieldOfView, aimFov, fovSpeed * Time.deltaTime);
        
            if (Math.Abs(weaponCamera.fieldOfView - aimFov) < 1f)
            {
                toggleCrosshairEvent.Raise(false);
            }

            if (!_hasSoundPlayed) 
            {
                mainAudioSource.Play();
                _hasSoundPlayed = true;
            }
            
            _animator.SetBool(AimAnimatorState, true);
            _isAds = true;
        }
        
        private void ReleaseAim()
        {
            weaponCamera.fieldOfView = Mathf.Lerp(weaponCamera.fieldOfView, defaultFov,fovSpeed * Time.deltaTime);
            toggleCrosshairEvent.Raise(true);

            _animator.SetBool(AimAnimatorState, false);
            _hasSoundPlayed = false;
            _isAds = false;
        }

        private void Fire()
        {
            _animator.Play(_isAds ? AimFireAnimationId : FireAnimationId, 0, 0f);
            shootAudioSource.Play();
            muzzleParticleSystem.Emit(1);

            DoMuzzleFlash();
            SpawnProjectile();
        }

        private void DoMuzzleFlash()
        {
            StartCoroutine(MuzzleFlashLightRoutine());
            muzzleParticleSystem.Emit (1);
        }
        
        private IEnumerator MuzzleFlashLightRoutine() 
        {
            muzzleFlash.enabled = true;
            yield return new WaitForSeconds(lightDuration);
            muzzleFlash.enabled = false;
        }

        private void SpawnProjectile()
        {
            var rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            var rayDirection = mainCamera.transform.forward;
            
            var intersects = Physics.Raycast(rayOrigin, rayDirection, out var hit, Mathf.Infinity);
            var destination = intersects ? hit.point : rayDirection * 1000;
            
            var firePoint = weaponBarrel.position;
            var projectile = Instantiate(projectilePrefab, firePoint, Quaternion.identity);
            var direction = (destination - firePoint).normalized;
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;

            var casingTransform = casingSpawn.transform;
            Instantiate(casingPrefab, casingTransform.position, casingTransform.rotation);
        }
        
        private void CheckIfTargetHovered()
        {
            var rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            var rayDirection = mainCamera.transform.forward;
            
            if (Physics.Raycast(rayOrigin, rayDirection, out var hit, Mathf.Infinity, TargetLayerMask))
            {
                Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.yellow);
                var healthBehaviour = hit.transform.gameObject.GetComponent<HealthBehaviour>();
                if (healthBehaviour != null)
                {
                    healthBehaviour.IsHovered = true;
                }
            }
            else
            {
                Debug.DrawRay(rayOrigin, rayDirection * 1000, Color.white);
            }
        }
    }
}