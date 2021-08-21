using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly Vector2 ScenarioButtonOffset = new Vector2(550, -150);
        private static readonly Color RedTextColour = new Color(0.8588236f, 0.2235294f, 0.3098039f);
        
        [SerializeField] private InitMainMenuEvent initMainMenuEvent;
        [SerializeField] private VoidEvent selectPlayTabEvent;
        [SerializeField] private VoidEvent selectStatsTabEvent;
        [SerializeField] private VoidEvent selectSettingsTabEvent;
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        
        [SerializeField] private Transform scenarioButtonPrefab;
        [SerializeField] private GameObject playPanel;
        [SerializeField] private TextMeshProUGUI playText;
        [SerializeField] private TextMeshProUGUI statsText;
        [SerializeField] private TextMeshProUGUI settingsText;
        
        public void OnEnable()
        {
            initMainMenuEvent.OnRaised += Initialise;
            selectPlayTabEvent.OnRaised += OnPlayTabSelect;
            selectStatsTabEvent.OnRaised += OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised += OnSettingsTabSelect;
        }

        public void OnDisable()
        {
            initMainMenuEvent.OnRaised -= Initialise;
            selectPlayTabEvent.OnRaised -= OnPlayTabSelect;
            selectStatsTabEvent.OnRaised -= OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised -= OnSettingsTabSelect;
        }

        private void Initialise(IEnumerable<Scenario.Scenario> scenarios)
        {
            CreateScenarioButtons(scenarios);
            selectPlayTabEvent.Raise();
        }

        private void CreateScenarioButtons(IEnumerable<Scenario.Scenario> scenarios)
        {
            var xOffset = -ScenarioButtonOffset.x;
            
            foreach (var scenario in scenarios)
            {
                var button = Instantiate(scenarioButtonPrefab, Vector3.zero, Quaternion.identity);
                button.SetParent(playPanel.transform, false);
                button.Translate(xOffset, ScenarioButtonOffset.y, 0);
                
                button.GetComponentInChildren<TextMeshProUGUI>().text = scenario.Name.ToUpper();
                button.GetComponent<Button>().onClick.AddListener(delegate
                {
                    gameObject.SetActive(false);
                    playScenarioEvent.Raise(scenario);
                });
                
                xOffset += ScenarioButtonOffset.x;
            }
        }

        private void OnPlayTabSelect()
        {
            DeselectTabs();
            playText.color = RedTextColour;
            playPanel.SetActive(true);
        }

        private void OnStatsTabSelect()
        {
            DeselectTabs();
            statsText.color = RedTextColour;
            // TODO: stats panel
        }

        private void OnSettingsTabSelect()
        {
            DeselectTabs();
            settingsText.color = RedTextColour;
            // TODO: settings panel
        }

        private void DeselectTabs()
        {
            playText.color = Color.white;
            statsText.color = Color.white;
            settingsText.color = Color.white;
            playPanel.SetActive(false);
        }
    }
}