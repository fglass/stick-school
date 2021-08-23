using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        private Image image;
        private Material defaultMaterial;
        private Material accentMaterial;

        public void Awake()
        {
            image = GetComponent<Image>();
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
            GetComponent<Image>().material = defaultMaterial;
        }

        private void SetAccentMaterial()
        {
            image.material = accentMaterial;
        }

        private void SetDefaultMaterial()
        {
            image.material = defaultMaterial;
        }
    }
}