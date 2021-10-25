using Events;
using UnityEngine;

namespace Scenario.Target
{
    public class HealthBehaviour : MonoBehaviour
    {
        [SerializeField] private VoidEvent targetHitEvent;

        private static readonly int ColourId = Shader.PropertyToID("TargetColour");
        private static readonly Color HoveredColour = new Color(0.04705881f, 0.6039216f, 0.1733971f);

        private TargetController _targetController;
        private AudioSource _audioSource;
        private Material _material;
        private Color _defaultColour;
        private int _health = 75;

        public bool IsHovered { get; set; }

        public void Awake()
        {
            _targetController = GetComponent<TargetController>();
            _audioSource = GetComponent<AudioSource>();
            _material = GetComponent<MeshRenderer>().material;
            _defaultColour = _material.GetColor(ColourId);
        }
        
        public void FixedUpdate()
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
                _material.SetColor(ColourId, HoveredColour);
            } 
            else if (!IsHovered && IsHoveredMaterial)
            {
                _material.SetColor(ColourId, _defaultColour);
            }
        }

        private void CheckHealth()
        {
            if (_health <= 0)
            {
                _targetController.IsHit = true;
                targetHitEvent.Raise();
            } 
        }

        private bool IsDefaultMaterial => _material.GetColor(ColourId) == _defaultColour;

        private bool IsHoveredMaterial => _material.GetColor(ColourId) == HoveredColour;
    }
}