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
        private const int NGridColumns = 3;
        private static readonly Vector2 GridStartPosition = new Vector2(-550, 175);
        private static readonly Vector2 GridSpacing = new Vector2(550, -400);

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

        private int _selectedTabIndex;

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
            SelectTab(_selectedTabIndex);
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

        private void Initialise(IReadOnlyList<Scenario.Scenario> scenarios)
        {
            CreateScenarioButtons(scenarios);
            SelectTab(_selectedTabIndex);
        }

        private void CreateScenarioButtons(IReadOnlyList<Scenario.Scenario> scenarios)
        {
            var trainTab = tabs[1];

            for (var i = 0; i < scenarios.Count; i++)
            {
                var button = i == 0 ? trainTab.transform.GetChild(0) : Instantiate(
                    scenarioButtonPrefab, Vector3.zero, Quaternion.identity
                );
                button.SetParent(trainTab.transform, false);

                var column = i % NGridColumns;
                var row = i / NGridColumns;
                var x = GridStartPosition.x + column * GridSpacing.x;
                var y = GridStartPosition.y + row * GridSpacing.y;
                button.Translate(x, y, 0);
                
                var scenario = scenarios[i];
                button.GetComponentInChildren<TextMeshProUGUI>().text = scenario.Name.ToUpper();
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    gameObject.SetActive(false);
                    playScenarioEvent.Raise(scenario);
                });
            }
        }
        
        public void Update()
        {
            if (InputManager.IsLeftMenuNavigationPressed())
            {
                var previousTabIndex = Mod(_selectedTabIndex - 1, tabs.Length);
                SelectTab(previousTabIndex);
            } else if (InputManager.IsRightMenuNavigationPressed())
            {
                var nextTabIndex = Mod(_selectedTabIndex + 1, tabs.Length);
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
            _selectedTabIndex = index;
            tabTexts[_selectedTabIndex].GetComponent<TextInteraction>().Select();

            var selectedTab = tabs[_selectedTabIndex];
            selectedTab.SetActive(true);
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