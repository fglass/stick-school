using Scenes.Range.Components.Scripts.Game.Event;
using TMPro;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.UI
{
    public class CanvasController : MonoBehaviour
    {
        private TextMeshProUGUI _score;
        private TextMeshProUGUI _timer;
        private GameObject _crosshair;

        public void Start()
        {
            _score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
            _timer = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
            _crosshair = transform.Find("Crosshair").gameObject;
        }

        public void OnEnable()
        {
            EventManager.OnTargetHit += IncrementScore;
        }

        public void OnDisable()
        {
            EventManager.OnTargetHit -= IncrementScore;
        }
        
        private void IncrementScore()
        {
            var score = int.Parse(_score.text);
            SetScore(++score);
        }
        
        public void SetScore(int score)
        {
            _score.text = score.ToString();
        }
        
        public void SetTimer(int seconds)
        {
            _timer.text = $"{seconds / 60}:{seconds % 60:00}";
        }

        public void ToggleCrosshair(bool enable)
        {
            _crosshair.SetActive(enable);
        }
    }
}