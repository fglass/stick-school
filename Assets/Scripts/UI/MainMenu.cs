using System.Collections.Generic;
using Events;
using Input;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly Vector2 ScenarioButtonOffset = new Vector2(550, 175);

        [SerializeField] private InitMainMenuEvent initMainMenuEvent;
        [SerializeField] private VoidEvent selectHomeTabEvent;
        [SerializeField] private VoidEvent selectTrainTabEvent;
        [SerializeField] private VoidEvent selectProfileTabEvent;
        [SerializeField] private VoidEvent selectSettingsTabEvent;
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        
        [SerializeField] private GameObject[] tabs;
        [SerializeField] private TextMeshProUGUI[] tabTexts;
        [SerializeField] private GameObject controllerNavigation;
        [SerializeField] private GameObject controllerExitButton;
        [SerializeField] private Transform scenarioButtonPrefab;

        private int selectedTabIndex;

        public void Awake()
        {
            OnInputChange(InputManager.IsUsingController);
        }

        public void OnEnable()
        {
            initMainMenuEvent.OnRaised += Initialise;
            selectHomeTabEvent.OnRaised += OnHomeTabSelect;
            selectTrainTabEvent.OnRaised += OnTrainTabSelect;
            selectProfileTabEvent.OnRaised += OnProfileTabSelect;
            selectSettingsTabEvent.OnRaised += OnSettingsTabSelect;
            InputManager.InputChangeEvent += OnInputChange;
            SelectTab(selectedTabIndex);
        }

        public void OnDisable()
        {
            initMainMenuEvent.OnRaised -= Initialise;
            selectHomeTabEvent.OnRaised -= OnHomeTabSelect;
            selectTrainTabEvent.OnRaised -= OnTrainTabSelect;
            selectProfileTabEvent.OnRaised -= OnProfileTabSelect;
            selectSettingsTabEvent.OnRaised -= OnSettingsTabSelect;
            InputManager.InputChangeEvent -= OnInputChange;
        }

        private void Initialise(IEnumerable<Scenario.Scenario> scenarios)
        {
            CreateScenarioButtons(scenarios);
            SelectTab(selectedTabIndex);
        }

        private void CreateScenarioButtons(IEnumerable<Scenario.Scenario> scenarios)
        {
            var trainTab = tabs[1];
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
                var previousTabIndex = Mod(selectedTabIndex - 1, tabs.Length);
                SelectTab(previousTabIndex);
            } else if (InputManager.IsRightMenuNavigationPressed())
            {
                var nextTabIndex = Mod(selectedTabIndex + 1, tabs.Length);
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

        private void OnProfileTabSelect()
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
            tabTexts[selectedTabIndex].GetComponent<TextInteraction>().Select();

            var selectedTab = tabs[selectedTabIndex];
            selectedTab.SetActive(true);

            if (InputManager.IsUsingController)
            {
                var trainTab = tabs[1];
                var selectedObject = selectedTab == trainTab ? selectedTab.transform.GetChild(0).gameObject : null;
                EventSystem.current.SetSelectedGameObject(selectedObject);
            }
        }

        private void DeselectTabs()
        {
            for (var i = 0; i < tabs.Length; i++)
            {
                tabs[i].SetActive(false);
                tabTexts[i].GetComponent<TextInteraction>().Deselect();
            }
        }
        
        private void OnInputChange(bool isUsingController)
        {
            controllerNavigation.SetActive(isUsingController);
            controllerExitButton.SetActive(isUsingController);
        }

        private static int Mod(int n, int m)
        {
            return (n %= m) < 0 ? n + m : n;
        }
    }
}