using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class HealthBehaviour : MonoBehaviour
    {
        private TargetBehaviour _targetBehaviour;
        private Material _material;
        private Color _defaultColour;
        private readonly Color _hoveredColour = new Color(0.04705881f, 0.6039216f, 0.1733971f);
        private int _health = 75;

        public bool IsHovered { get; set; }

        public void Awake()
        {
            _targetBehaviour = GetComponent<TargetBehaviour>();
            _material = GetComponent<MeshRenderer>().material;
            _defaultColour = _material.color;
        }
        
        public void FixedUpdate() // TODO: order guaranteed?
        {
            TryDecrementHealth();
            TryChangeMaterialColour();
            CheckHealth();
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
            
            IsHovered = false;
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