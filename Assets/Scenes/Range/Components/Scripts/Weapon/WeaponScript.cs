using System;
using System.Collections;
using Scenes.Range.Components.Scripts.Controllers.Input;
using Scenes.Range.Components.Scripts.Game.Target;
using Scenes.Range.Components.Scripts.Game.UI;
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
        [SerializeField] private Camera weaponCamera;
        [SerializeField] private CanvasController canvasController;
        [SerializeField] private Light muzzleFlash;
        [SerializeField] private ParticleSystem muzzleParticleSystem;
        
        [SerializeField] private Transform projectilePrefab;
        [SerializeField] private Transform casingPrefab;
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
            if (input.IsAds()) 
            {
                AimDownSight();
            }
            else
            {
                ReleaseAim();
            }
            
            if (CanFire && input.IsFiring())
            {
                Fire();
            }
        }

        public void FixedUpdate()
        {
            CheckIfTargetHovered();
        }

        private void AimDownSight()
        {
            weaponCamera.fieldOfView = Mathf.Lerp (weaponCamera.fieldOfView, aimFov, fovSpeed * Time.deltaTime);
        
            if (Math.Abs(weaponCamera.fieldOfView - aimFov) < 1f)
            {
                canvasController.ToggleCrosshair(false);
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
            canvasController.ToggleCrosshair(true);

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

        private void CheckIfTargetHovered()
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out var hit, Mathf.Infinity, TargetLayerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                var healthBehaviour = hit.transform.gameObject.GetComponent<HealthBehaviour>();
                if (healthBehaviour != null)
                {
                    healthBehaviour.IsHovered = true;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
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
            var projectileTransform = transform;
            var position = projectileTransform.TransformDirection(Vector3.forward);
            position.y += 1.54f; // TODO: more robust solution
            
            var projectile = Instantiate(projectilePrefab, position, projectileTransform.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * bulletForce;

            var casingTransform = casingSpawn.transform;
            Instantiate(casingPrefab, casingTransform.position, casingTransform.rotation);
        }
    }
}