using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TextInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private static readonly Color DefaultColour = Color.white;
        private Color _accentColour;
        private TextMeshProUGUI _text;
        private bool _selected;

        public void Awake()
        {
            _accentColour = Resources.Load<Material>("Materials/UIAccentMaterial").color;
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            SetAccentColour();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_selected)
            {
                SetDefaultColour();
            }
        }

        public void Select()
        {
            _selected = true;
            SetAccentColour();
        }

        public void Deselect()
        {
            OnDisable();
        }
        
        public void OnDisable()
        {

            _selected = false;
            SetDefaultColour();
        }

        private void SetAccentColour()
        {
            if (_text != null)
            {
                _text.color = _accentColour;
            }
        }

        private void SetDefaultColour()
        {
            if (_text != null)
            {
                _text.color = DefaultColour;
            }
        }
    }
}