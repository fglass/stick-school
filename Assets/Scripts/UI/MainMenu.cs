using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly Vector2 ScenarioButtonOffset = new Vector2(550, 0);
        private static readonly Color RedTextColour = new Color(0.8588236f, 0.2235294f, 0.3098039f);
        
        [SerializeField] private InitMainMenuEvent initMainMenuEvent;
        [SerializeField] private VoidEvent selectHomeTabEvent;
        [SerializeField] private VoidEvent selectTrainTabEvent;
        [SerializeField] private VoidEvent selectStatsTabEvent;
        [SerializeField] private VoidEvent selectSettingsTabEvent;
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        
        [SerializeField] private Transform scenarioButtonPrefab;
        [SerializeField] private GameObject homeTab;
        [SerializeField] private GameObject trainTab;
        [SerializeField] private GameObject statsTab;
        [SerializeField] private GameObject settingsTab;
        [SerializeField] private TextMeshProUGUI homeText;
        [SerializeField] private TextMeshProUGUI trainText;
        [SerializeField] private TextMeshProUGUI statsText;
        [SerializeField] private TextMeshProUGUI settingsText;
        
        public void OnEnable()
        {
            initMainMenuEvent.OnRaised += Initialise;
            selectHomeTabEvent.OnRaised += OnHomeTabSelect;
            selectTrainTabEvent.OnRaised += OnTrainTabSelect;
            selectStatsTabEvent.OnRaised += OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised += OnSettingsTabSelect;
        }

        public void OnDisable()
        {
            initMainMenuEvent.OnRaised -= Initialise;
            selectHomeTabEvent.OnRaised += OnHomeTabSelect;
            selectTrainTabEvent.OnRaised -= OnTrainTabSelect;
            selectStatsTabEvent.OnRaised -= OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised -= OnSettingsTabSelect;
        }

        private void Initialise(IEnumerable<Scenario.Scenario> scenarios)
        {
            CreateScenarioButtons(scenarios);
            selectHomeTabEvent.Raise();
        }

        private void CreateScenarioButtons(IEnumerable<Scenario.Scenario> scenarios)
        {
            var xOffset = -ScenarioButtonOffset.x;
            
            foreach (var scenario in scenarios)
            {
                var button = Instantiate(scenarioButtonPrefab, Vector3.zero, Quaternion.identity);
                button.SetParent(trainTab.transform, false);
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

        private void OnHomeTabSelect()
        {
            SelectTab(homeTab, homeText);
        }

        private void OnTrainTabSelect()
        {
            SelectTab(trainTab, trainText);
        }

        private void OnStatsTabSelect()
        {
            SelectTab(statsTab, statsText);
        }

        private void OnSettingsTabSelect()
        {
            SelectTab(settingsTab, settingsText);
        }

        private void SelectTab(GameObject tab, Graphic text)
        {
            DeselectTabs();
            tab.SetActive(true);
            text.color = RedTextColour;
        }

        private void DeselectTabs()
        {
            homeTab.SetActive(false);
            trainTab.SetActive(false);
            statsTab.SetActive(false);
            settingsTab.SetActive(false);
            homeText.color = Color.white;
            trainText.color = Color.white;
            statsText.color = Color.white;
            settingsText.color = Color.white;
        }
    }
}