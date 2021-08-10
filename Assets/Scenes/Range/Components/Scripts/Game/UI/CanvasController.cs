using Scenes.Range.Components.Scripts.Game.Event;
using TMPro;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.UI
{
    public class CanvasController : MonoBehaviour
    {
        private GameObject _crosshair;
        private TextMeshProUGUI _score;

        public void Start()
        {
            _crosshair = transform.Find("Crosshair").gameObject;
            _score = transform.Find("Score").GetComponent<TextMeshProUGUI>();
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
            var newScore = score + 1;
            SetScore(newScore);
        }

        public void ToggleCrosshair(bool enable)
        {
            _crosshair.SetActive(enable);
        }
        
        public void SetScore(int value)
        {
            _score.text = value.ToString();
        }
    }
}