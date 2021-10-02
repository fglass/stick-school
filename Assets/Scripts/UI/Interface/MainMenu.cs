using System.Collections.Generic;
using Events;
using Input;
using TMPro;
using UI.Behaviour;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Interface
{
    public class MainMenu : MonoBehaviour
    {
        private const int NGridColumns = 3;
        private static readonly Vector2 GridStartPosition = new Vector2(-550, 175);
        private static readonly Vector2 GridSpacing = new Vector2(550, -400);

        [SerializeField] private InitMainMenuEvent initMainMenuEvent;
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        [SerializeField] private TabCoordinator navBar;
        [SerializeField] private Transform scenarioButtonPrefab;
        [SerializeField] private GameObject[] controllerElements;

        public void Awake()
        {
            OnInputChange(InputManager.IsUsingController);
        }

        public void OnEnable()
        {
            initMainMenuEvent.OnRaised += Initialise;
            InputManager.InputChangeEvent += OnInputChange;
        }

        public void OnDisable()
        {
            initMainMenuEvent.OnRaised -= Initialise;
            InputManager.InputChangeEvent -= OnInputChange;
        }

        private void Initialise(IReadOnlyList<Scenario.Scenario> scenarios)
        {
            CreateScenarioButtons(scenarios);
            navBar.SelectTab(0);
        }

        private void CreateScenarioButtons(IReadOnlyList<Scenario.Scenario> scenarios)
        {
            var trainTab = navBar.GetTab(1);

            for (var i = 0; i < scenarios.Count; i++)
            {
                CreateScenarioButton(trainTab, scenarios[i], i);
            }
        }

        private void CreateScenarioButton(GameObject parent, Scenario.Scenario scenario, int index)
        {
            var button = index == 0 ? parent.transform.GetChild(0) : Instantiate(
                scenarioButtonPrefab, Vector3.zero, Quaternion.identity
            );
            button.SetParent(parent.transform, false);

            var column = index % NGridColumns;
            var row = index / NGridColumns;
            var x = GridStartPosition.x + column * GridSpacing.x;
            var y = GridStartPosition.y + row * GridSpacing.y;
            button.Translate(x, y, 0);
                
            button.GetComponentInChildren<TextMeshProUGUI>().text = scenario.Name.ToUpper();
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                playScenarioEvent.Raise(scenario);
            });
        }
        
        public void Update()
        {
            if (InputManager.IsLeftMenuNavigationPressed())
            {
                navBar.SelectPreviousTab();
            } else if (InputManager.IsRightMenuNavigationPressed())
            {
                navBar.SelectNextTab();
            }
        }

        private void OnInputChange(bool isUsingController)
        {
            foreach (var element in controllerElements)
            {
                element.SetActive(isUsingController);
            }
        }
    }
}