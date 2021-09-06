using System.Collections.Generic;
using Controller.Input;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

        private IList<GameObject> tabs;
        private IList<TextMeshProUGUI> tabTexts;
        private int selectedTabIndex;

        public void Awake()
        {
            tabs = new[] { homeTab, trainTab, statsTab, settingsTab };
            tabTexts = new[] { homeText, trainText, statsText, settingsText };
        }

        public void OnEnable()
        {
            initMainMenuEvent.OnRaised += Initialise;
            selectHomeTabEvent.OnRaised += OnHomeTabSelect;
            selectTrainTabEvent.OnRaised += OnTrainTabSelect;
            selectStatsTabEvent.OnRaised += OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised += OnSettingsTabSelect;
            SelectTab(selectedTabIndex);
        }

        public void OnDisable()
        {
            initMainMenuEvent.OnRaised -= Initialise;
            selectHomeTabEvent.OnRaised -= OnHomeTabSelect;
            selectTrainTabEvent.OnRaised -= OnTrainTabSelect;
            selectStatsTabEvent.OnRaised -= OnStatsTabSelect;
            selectSettingsTabEvent.OnRaised -= OnSettingsTabSelect;
        }

        private void Initialise(IEnumerable<Scenario.Scenario> scenarios)
        {
            CreateScenarioButtons(scenarios);
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
        
        public void Update()
        {
            if (InputManager.IsLeftMenuNavigationPressed())
            {
                var previousTabIndex = Mod(selectedTabIndex - 1, tabs.Count);
                SelectTab(previousTabIndex);
            } else if (InputManager.IsRightMenuNavigationPressed())
            {
                var nextTabIndex = Mod(selectedTabIndex + 1, tabs.Count);
                SelectTab(nextTabIndex);
            }
        }

        private void OnHomeTabSelect()
        {
            SelectTab(0);
        }

        private void OnTrainTabSelect()
        {
            SelectTab(1);
        }

        private void OnStatsTabSelect()
        {
            SelectTab(2);
        }

        private void OnSettingsTabSelect()
        {
            SelectTab(3);
        }

        private void SelectTab(int index)
        {
            DeselectTabs();
            selectedTabIndex = index;
            tabTexts[selectedTabIndex].color = RedTextColour;
            
            var selectedTab = tabs[selectedTabIndex];
            selectedTab.SetActive(true);

            if (InputManager.IsUsingController())
            {
                EventSystem.current.SetSelectedGameObject(
                    selectedTab == trainTab ? selectedTab.transform.GetChild(0).gameObject : null
                );
            }
        }

        private void DeselectTabs()
        {
            for (var i = 0; i < tabs.Count; i++)
            {
                tabs[i].SetActive(false);
                tabTexts[i].color = Color.white;
            }
        }

        private static int Mod(int n, int m)
        {
            return (n %= m) < 0 ? n + m : n;
        }
    }
}