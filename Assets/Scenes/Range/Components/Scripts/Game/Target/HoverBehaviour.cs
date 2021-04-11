using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Target
{
    public class HoverBehaviour : MonoBehaviour
    {
        private MeshRenderer _renderer;
        private Material _defaultMaterial;
        private Material _hoveredMaterial;
        private Material _currentMaterial;
        
        public bool IsHovered { get; set; }

        public void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _defaultMaterial = Resources.Load<Material>("Materials/TargetMaterial");
            _hoveredMaterial = Resources.Load<Material>("Materials/HoveredTargetMaterial");
            _currentMaterial = _defaultMaterial;
        }
        
        public void FixedUpdate() // TODO: order guaranteed?
        {
            TryUpdateMaterial();
        }
        
        private void TryUpdateMaterial()
        {
            if (IsHovered && _currentMaterial == _defaultMaterial)
            {
                _currentMaterial = _hoveredMaterial;
                _renderer.material = _currentMaterial;
            } 
            else if (!IsHovered && _currentMaterial == _hoveredMaterial)
            {
                _currentMaterial = _defaultMaterial;
                _renderer.material = _currentMaterial;
            }
            
            IsHovered = false;
        }
    }
}