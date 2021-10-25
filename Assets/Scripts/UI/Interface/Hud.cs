using Events;
using TMPro;
using UnityEngine;

namespace UI.Interface
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private TextMeshProUGUI accuracyText;
        [SerializeField] private GameObject crosshair;

        [SerializeField] private IntEvent setHudTimerEvent;
        [SerializeField] private IntEvent setHudScoreEvent;
        [SerializeField] private IntEvent setHudAccuracyEvent;
        [SerializeField] private BoolEvent toggleCrosshairEvent;


        public void OnEnable()
        {
            setHudTimerEvent.OnRaised += SetTimer;
            setHudScoreEvent.OnRaised += SetScore;
            setHudAccuracyEvent.OnRaised += SetAccuracy;
            toggleCrosshairEvent.OnRaised += ToggleCrosshair;
        }

        public void OnDisable()
        {
            setHudTimerEvent.OnRaised -= SetTimer;
            setHudScoreEvent.OnRaised -= SetScore;
            setHudAccuracyEvent.OnRaised -= SetAccuracy;
            toggleCrosshairEvent.OnRaised -= ToggleCrosshair;
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