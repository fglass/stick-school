using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private TextMeshProUGUI text;
        private static readonly Color DefaultColour = Color.white;
        private Color accentColour;
        private bool selected;

        public void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            accentColour = Resources.Load<Material>("Materials/UIAccentMaterial").color;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetAccentColour();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!selected)
            {
                SetDefaultColour();
            }
        }

        public void Select()
        {
            selected = true;
            SetAccentColour();
        }

        public void Deselect()
        {
            OnDisable();
        }
        
        public void OnDisable()
        {

            selected = false;
            SetDefaultColour();
        }

        private void SetAccentColour()
        {
            text.color = accentColour;
        }

        private void SetDefaultColour()
        {
            text.color = DefaultColour;
        }
    }
}