using System.Collections.Generic;
using Game.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        private static readonly Color Red = new Color(0.8588236f, 0.2235294f, 0.3098039f);
        [SerializeField] private Transform scenarioButtonPrefab;
       
        private GameObject _mainMenu;
        private GameObject _playPanel;
        
        private TextMeshProUGUI _playText;
        private TextMeshProUGUI _statsText;
        private TextMeshProUGUI _settingsText;

        public void Awake()
        {
            _mainMenu = transform.Find("MainMenu").gameObject;
            _playPanel = _mainMenu.transform.Find("PlayPanel").gameObject;

            var navBar = _mainMenu.transform.Find("NavBar");
            _playText = navBar.Find("PlayButton").Find("Text").GetComponent<TextMeshProUGUI>();
            _statsText = navBar.Find("StatsButton").Find("Text").GetComponent<TextMeshProUGUI>();
            _settingsText = navBar.Find("SettingsButton").Find("Text").GetComponent<TextMeshProUGUI>();
            
            OnPlay();
        }

        public void CreateScenarioButtons(IEnumerable<Scenario.Scenario> scenarios)
        {
            var xOffset = -550;
            
            foreach (var scenario in scenarios)
            {
                var button = Instantiate(scenarioButtonPrefab, Vector3.zero, Quaternion.identity);
                button.SetParent(_playPanel.transform, false);
                button.Translate(xOffset, 0, 0);
                
                button.Find("ScenarioName").GetComponent<TextMeshProUGUI>().text = scenario.Name.ToUpper();
                button.GetComponent<Button>().onClick.AddListener(delegate { OnScenario(scenario); });
                
                xOffset += 550;
            }
        }

        public void OnPlay()
        {
            DeselectTabs();
            _playText.color = Red;
            _playPanel.SetActive(true);
        }

        public void OnStats()
        {
            DeselectTabs();
            _statsText.color = Red;
            Debug.Log("Open stats");
        }

        public void OnSettings()
        {
            DeselectTabs();
            _settingsText.color = Red;
            Debug.Log("Open settings");
        }

        public void OnQuit()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
        
        private void DeselectTabs()
        {
            _playText.color = Color.white;
            _statsText.color = Color.white;
            _settingsText.color = Color.white;
            _playPanel.SetActive(false);
        }
        
        private void OnScenario(Scenario.Scenario scenario)
        {
            _mainMenu.SetActive(false);
            EventBus.PublishPlay(scenario);
        }
    }
}