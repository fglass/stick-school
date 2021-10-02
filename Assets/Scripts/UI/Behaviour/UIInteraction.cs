using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        [SerializeField] private Image targetImage;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material accentMaterial;

        public void Awake()
        {
            if (targetImage == null)
            {
                targetImage = GetComponent<Image>();
            }

            if (defaultMaterial == null)
            {
                defaultMaterial = Resources.Load<Material>("Materials/UIPrimaryMaterial");
            }

            if (accentMaterial == null)
            {
                accentMaterial = Resources.Load<Material>("Materials/UIAccentMaterial");
            }
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
            SetDefaultMaterial();
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