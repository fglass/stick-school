using System.Globalization;
using TMPro;
using UnityEngine;

namespace UI
{
    public class FpsCounter : MonoBehaviour
    {
        private TextMeshProUGUI field;

        public void Awake()
        {
            field = GetComponent<TextMeshProUGUI>();
        }

        public void Update() {
            var fps = 1 / Time.unscaledDeltaTime;
            field.text = Mathf.Ceil(fps).ToString(CultureInfo.InvariantCulture);
        }
    }
}