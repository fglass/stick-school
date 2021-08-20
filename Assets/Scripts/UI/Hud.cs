using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private BoolEvent toggleCrosshairEvent;
        [SerializeField] private IntEvent setHudTimerEvent;
        [SerializeField] private IntEvent setHudScoreEvent;
        [SerializeField] private IntEvent setHudAccuracyEvent;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI accuracyText;
        [SerializeField] private GameObject crosshair;

        public void OnEnable()
        {
            toggleCrosshairEvent.OnRaised += ToggleCrosshair;
            setHudTimerEvent.OnRaised += SetTimer;
            setHudScoreEvent.OnRaised += SetScore;
            setHudAccuracyEvent.OnRaised += SetAccuracy;
        }

        public void OnDisable()
        {
            toggleCrosshairEvent.OnRaised -= ToggleCrosshair;
            setHudTimerEvent.OnRaised -= SetTimer;
            setHudScoreEvent.OnRaised -= SetScore;
            setHudAccuracyEvent.OnRaised -= SetAccuracy;
        }
        
        private void SetTimer(int seconds)
        {
            timerText.text = $"{seconds / 60}:{seconds % 60:00}";
        }

        private void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }

        private void SetAccuracy(int accuracy)
        {
            accuracyText.text = $"{accuracy}%";
        }

        private void ToggleCrosshair(bool enable)
        {
            crosshair.SetActive(enable);
        }
    }
}