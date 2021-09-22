using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class TextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static readonly Color DefaultColour = Color.white;
        private Color accentColour;
        private TextMeshProUGUI text;
        private bool selected;

        public void Awake()
        {
            accentColour = Resources.Load<Material>("Materials/UIAccentMaterial").color;
            text = GetComponent<TextMeshProUGUI>();
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
            if (text != null)
            {
                text.color = accentColour;
            }
        }

        private void SetDefaultColour()
        {
            if (text != null)
            {
                text.color = DefaultColour;
            }
        }
    }
}