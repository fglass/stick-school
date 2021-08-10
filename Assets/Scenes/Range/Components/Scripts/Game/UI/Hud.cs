using TMPro;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.UI
{
    public class Hud : MonoBehaviour
    {
        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _timerText;
        private TextMeshProUGUI _accuracyText;
        private GameObject _crosshair;

        public void Awake()
        {
            _scoreText = transform.Find("Score").GetComponent<TextMeshProUGUI>();
            _timerText = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
            _accuracyText = transform.Find("Accuracy").GetComponent<TextMeshProUGUI>();
            _crosshair = transform.Find("Crosshair").gameObject;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void SetScore(int score)
        {
            _scoreText.text = score.ToString();
        }
        
        public void SetTimer(int seconds)
        {
            _timerText.text = $"{seconds / 60}:{seconds % 60:00}";
        }

        public void SetAccuracy(int accuracy)
        {
            _accuracyText.text = $"{accuracy}%";
        }

        public void ToggleCrosshair(bool enable)
        {
            _crosshair.SetActive(enable);
        }
    }
}