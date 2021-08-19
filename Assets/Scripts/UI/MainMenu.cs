using System;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly Color Red = new Color(0.8588236f, 0.2235294f, 0.3098039f);

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
            selectPlayTabEvent.OnRaised += OnPlayTabSelect;
            selectStatsTabEvent.OnRaised += OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised += OnSettingsTabSelect;
        }

        public void OnDisable()
        {
            selectPlayTabEvent.OnRaised -= OnPlayTabSelect;
            selectStatsTabEvent.OnRaised -= OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised -= OnSettingsTabSelect;
        }
        
        public void Start()
        {
            selectPlayTabEvent.Raise();
        }

        public void CreateScenarioButtons(IEnumerable<Scenario.Scenario> scenarios) // TODO: event
        {
            var xOffset = -550;
            
            foreach (var scenario in scenarios)
            {
                var button = Instantiate(scenarioButtonPrefab, Vector3.zero, Quaternion.identity);
                button.SetParent(playPanel.transform, false);
                button.Translate(xOffset, 0, 0);
                
                button.Find("ScenarioName").GetComponent<TextMeshProUGUI>().text = scenario.Name.ToUpper();
                button.GetComponent<Button>().onClick.AddListener(delegate { OnScenario(scenario); });
                
                xOffset += 550;
            }
        }

        private void OnPlayTabSelect()
        {
            DeselectTabs();
            playText.color = Red;
            playPanel.SetActive(true);
        }

        private void OnStatsTabSelect()
        {
            DeselectTabs();
            statsText.color = Red;
            // TODO: stats panel
        }

        private void OnSettingsTabSelect()
        {
            DeselectTabs();
            settingsText.color = Red;
            // TODO: settings panel
        }

        private void DeselectTabs()
        {
            playText.color = Color.white;
            statsText.color = Color.white;
            settingsText.color = Color.white;
            playPanel.SetActive(false);
        }
        
        private void OnScenario(Scenario.Scenario scenario)
        {
            gameObject.SetActive(false);
            playScenarioEvent.Raise(scenario);
        }
    }
}