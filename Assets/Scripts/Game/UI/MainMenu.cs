using System.Collections.Generic;
using Game.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Transform scenarioButtonPrefab;
        private GameObject _mainMenu;
        private GameObject _playPanel;

        public void Awake()
        {
            _mainMenu = transform.Find("MainMenu").gameObject;
            _playPanel = _mainMenu.transform.Find("PlayPanel").gameObject;
        }

        public void CreateScenarioButtons(IEnumerable<Scenario.Scenario> scenarios)
        {
            var xOffset = -400;
            
            foreach (var scenario in scenarios)
            {
                var button = Instantiate(scenarioButtonPrefab, Vector3.zero, Quaternion.identity);
                button.SetParent(_playPanel.transform, false);
                button.Translate(xOffset, 0, 0);
                
                button.Find("ScenarioName").GetComponent<TextMeshProUGUI>().text = scenario.Name.ToUpper();
                button.GetComponent<Button>().onClick.AddListener(delegate { SelectScenario(scenario); });
                
                xOffset += 450;
            }
        }

        public void OnPlay()
        {
            // TODO: tab selecting
            _playPanel.SetActive(true);
        }

        public void OnStats()
        {
            _playPanel.SetActive(false);
            Debug.Log("Open stats");
        }

        public void OnSettings()
        {
            _playPanel.SetActive(false);
            Debug.Log("Open settings");
        }

        public void OnQuit()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
        
        private void SelectScenario(Scenario.Scenario scenario)
        {
            _mainMenu.SetActive(false);
            EventBus.PublishPlay(scenario);
        }
    }
}