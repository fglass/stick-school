using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Image targetImage;
        private Material defaultMaterial;
        private Material accentMaterial;

        public void Awake()
        {
            if (targetImage == null)
            {
                targetImage = GetComponent<Image>();
            }
            defaultMaterial = Resources.Load<Material>("Materials/UIPrimaryMaterial");
            accentMaterial = Resources.Load<Material>("Materials/UIAccentMaterial");
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetAccentMaterial();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetDefaultMaterial();
        }
        
        public void OnSelect(BaseEventData eventData)
        {
            SetAccentMaterial();
        }
        
        public void OnDeselect(BaseEventData eventData)
        {
            SetDefaultMaterial();
        }

        public void OnDisable()
        {
            targetImage.material = defaultMaterial;
        }

        private void SetAccentMaterial()
        {
            targetImage.material = accentMaterial;
        }

        private void SetDefaultMaterial()
        {
            targetImage.material = defaultMaterial;
        }
    }
}