using Game.Event;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ResultsPanel : MonoBehaviour
    {
        private Transform _modalCanvas;
        private Transform _mainMenuCanvas;
        private TextMeshProUGUI _scenarioName;
        private TextMeshProUGUI _scoreField;
        private TextMeshProUGUI _accuracyField;
        
        public void Awake()
        {
            _modalCanvas = transform.Find("ResultsPanel");
            _mainMenuCanvas = transform.Find("MainMenu");
            
            var panel = _modalCanvas.Find("Panel");
            _scenarioName = panel.Find("ScenarioName").GetComponent<TextMeshProUGUI>();
            _scoreField = panel.Find("ScoreField").GetComponent<TextMeshProUGUI>();
            _accuracyField = panel.Find("AccuracyField").GetComponent<TextMeshProUGUI>();
        }

        public void Display(string scenarioName, int score, int hitShots, int missedShots, int accuracy)
        {
            _scenarioName.text = $"{scenarioName} Scenario";
            _scoreField.text = score.ToString();
            _accuracyField.text = $"{hitShots}/{hitShots + missedShots} ({accuracy}%)";
            _modalCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
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