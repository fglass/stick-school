using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultsPanel : MonoBehaviour
    {
        [SerializeField] private VoidEvent restartScenarioEvent;
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private TextMeshProUGUI titleField;
        [SerializeField] private TextMeshProUGUI scoreField;
        [SerializeField] private TextMeshProUGUI accuracyField;

        public void OnEnable()
        {
            restartScenarioEvent.OnRaised += Close;
            openMainMenuEvent.OnRaised += Close;
        }

        public void OnDisable()
        {
            restartScenarioEvent.OnRaised -= Close;
            openMainMenuEvent.OnRaised -= Close;
        }

        public void Display(string scenarioName, int score, int hitShots, int missedShots, int accuracy) // TODO: event
        {
            titleField.text = $"{scenarioName} Scenario";
            scoreField.text = score.ToString();
            accuracyField.text = $"{hitShots}/{hitShots + missedShots} ({accuracy}%)";
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}