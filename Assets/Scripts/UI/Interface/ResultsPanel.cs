using Events;
using Scenario;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ResultsPanel : MonoBehaviour
    {
        [SerializeField] private DisplayResultsEvent displayResultsEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;
        [SerializeField] private VoidEvent openMainMenuEvent;
        
        [SerializeField] private TextMeshProUGUI titleField;
        [SerializeField] private TextMeshProUGUI scoreField;
        [SerializeField] private TextMeshProUGUI accuracyField;

        public void OnEnable()
        {
            displayResultsEvent.OnRaised += Display;
            restartScenarioEvent.OnRaised += Close;
            openMainMenuEvent.OnRaised += Close;
        }

        public void OnDisable()
        {
            displayResultsEvent.OnRaised -= Display;
            restartScenarioEvent.OnRaised -= Close;
            openMainMenuEvent.OnRaised -= Close;
        }

        private void Display(ScoreHandler.ResultsDto results)
        {
            titleField.text = $"{results.ScenarioName.ToUpper()} SCENARIO";
            scoreField.text = results.Score.ToString();
            accuracyField.text = $"{results.HitShots}/{results.HitShots + results.MissedShots} ({results.Accuracy}%)";
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}