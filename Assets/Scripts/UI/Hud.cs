using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class Hud : MonoBehaviour
    {
        private Transform _canvas;
        private TextMeshProUGUI _scoreText;
        private TextMeshProUGUI _timerText;
        private TextMeshProUGUI _accuracyText;
        private GameObject _crosshair;

        public void Awake()
        {
            _canvas = transform.Find("HUD");
            _scoreText = _canvas.Find("Score").GetComponent<TextMeshProUGUI>();
            _timerText = _canvas.Find("Timer").GetComponent<TextMeshProUGUI>();
            _accuracyText = _canvas.Find("Accuracy").GetComponent<TextMeshProUGUI>();
            _crosshair = _canvas.Find("Crosshair").gameObject;
        }

        public void Toggle(bool enable)
        {
            _canvas.gameObject.SetActive(enable);
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