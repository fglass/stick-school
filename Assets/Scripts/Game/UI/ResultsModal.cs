using Game.Event;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ResultsModal : MonoBehaviour
    {
        private Transform _modalCanvas;
        private Transform _mainMenuCanvas;
        private TextMeshProUGUI _scenarioName;
        private TextMeshProUGUI _scoreField;
        private TextMeshProUGUI _accuracyField;
        
        public void Awake()
        {
            _modalCanvas = transform.Find("ResultsModal");
            _mainMenuCanvas = transform.Find("MainMenu");
            
            var panel = _modalCanvas.Find("Panel");
            _scenarioName = panel.Find("ScenarioName").GetComponent<TextMeshProUGUI>();
            _scoreField = panel.Find("ScoreField").GetComponent<TextMeshProUGUI>();
            _accuracyField = panel.Find("AccuracyField").GetComponent<TextMeshProUGUI>();
        }

        public void Display(string scenarioName, int score, int hitShots, int missedShots, int accuracy)
        {
            Cursor.lockState = CursorLockMode.None;
            _modalCanvas.gameObject.SetActive(true);
            _scenarioName.text = scenarioName;
            _scoreField.text = score.ToString();
            _accuracyField.text = $"{hitShots}/{hitShots + missedShots} ({accuracy}%)";
            EventBus.PublishPause(); // TODO: change?
        }

        public void OnRestart()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _modalCanvas.gameObject.SetActive(false);
            EventBus.PublishPlay();
        }

        public void OnMenu()
        {
            _modalCanvas.gameObject.SetActive(false);
            _mainMenuCanvas.gameObject.SetActive(true);
        }
    }
}