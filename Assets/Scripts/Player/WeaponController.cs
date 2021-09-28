using System;
using Events;
using Input;
using Scenario.Target;
using UnityEngine;

namespace Player
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private float fovSpeed = 15.0f;
        [SerializeField] private float defaultFov = 40.0f;
        [SerializeField] private float aimFov = 15.0f;
        [SerializeField] private float projectileSpeed = 400;

        [SerializeField] private BoolEvent toggleCrosshairEvent;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera weaponCamera;
        
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform weaponBarrel;

        [SerializeField] private AudioSource mainAudioSource;
        [SerializeField] private AudioSource shootAudioSource;

        private const int TargetLayerMask = 1 << 9;
        private const string AimFireAnimationID = "Aim Fire";
        private const string FireAnimationID = "Fire";
        private static readonly int AimAnimatorState = Animator.StringToHash("Aim");
        
        private Animator _animator;
        private bool _isAds;
        private bool _hasSoundPlayed;

        public bool CanFire { get; set; } = true;

        public void Start()
        {
            _animator = GetComponent<Animator>();
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
            _animator.Play(_isAds ? AimFireAnimationID : FireAnimationID, 0, 0f);
            shootAudioSource.Play();
            SpawnProjectile();
        }
        
        private void SpawnProjectile()
        {
            var firePoint = weaponBarrel.position;
            var projectile = Instantiate(projectilePrefab, firePoint, Quaternion.identity);
            
            var rayOrigin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            var rayDirection = mainCamera.transform.forward;
            var intersects = Physics.Raycast(rayOrigin, rayDirection, out var hit, Mathf.Infinity);
            var destination = intersects ? hit.point : rayDirection * 1000;
            var direction = (destination - firePoint).normalized;
            
            projectile.GetComponent<Rigidbody>().velocity = direction * projectileSpeed;
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