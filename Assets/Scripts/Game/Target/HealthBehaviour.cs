using Scenes.Range.Components.Scripts.Game.Target;
using UnityEngine;

namespace Game.Target
{
    public class HealthBehaviour : MonoBehaviour
    {
        private const string HoverSoundPath = "Audio/ding";
        private readonly Color _hoveredColour = new Color(0.04705881f, 0.6039216f, 0.1733971f);

        private TargetBehaviour _targetBehaviour;
        private AudioSource _audioSource;
        private Material _material;
        private Color _defaultColour;
        private int _health = 75;

        public bool IsHovered { get; set; }

        public void Awake()
        {
            _targetBehaviour = GetComponent<TargetBehaviour>();

            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = Resources.Load<AudioClip>(HoverSoundPath);
            _audioSource.bypassReverbZones = true;
            
            _material = GetComponent<MeshRenderer>().material;
            _defaultColour = _material.color;
        }
        
        public void FixedUpdate() // TODO: order guaranteed?
        {
            TryPlaySound();
            TryDecrementHealth();
            TryChangeMaterialColour();
            CheckHealth();
            IsHovered = false;
        }

        private void TryPlaySound()
        {
            if (IsHovered && !_audioSource.isPlaying )
            {
                _audioSource.Play();
            }
        }

        private void TryDecrementHealth()
        {
            if (IsHovered)
            {
                _health--;
            }
        }
        
        private void TryChangeMaterialColour()
        {
            if (IsHovered && IsDefaultMaterial)
            {
                _material.color = _hoveredColour;
            } 
            else if (!IsHovered && IsHoveredMaterial)
            {
                _material.color = _defaultColour;
            }
        }

        private void CheckHealth()
        {
            if (_health <= 0)
            {
                _targetBehaviour.IsHit = true;
            } 
        }

        private bool IsDefaultMaterial => _material.color == _defaultColour;

        private bool IsHoveredMaterial => _material.color == _hoveredColour;
    }
}