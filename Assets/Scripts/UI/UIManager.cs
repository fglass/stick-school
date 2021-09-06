using System.Collections;
using Controller.Input;
using Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private BoolEvent toggleHudEvent;
        [SerializeField] private VoidEvent openResultsPanelEvent;

        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent quitEvent;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private GameObject hud;

        private bool _paused;
        private bool IsInterfaceOpen => mainMenu.activeSelf || resultsPanel.activeSelf;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OpenMainMenu;
            toggleHudEvent.OnRaised += ToggleHud;
            openResultsPanelEvent.OnRaised += OpenResultsPanel;
            pauseScenarioEvent.OnRaised += OnPause;
            resumeScenarioEvent.OnRaised += OnResume;
            quitEvent.OnRaised += OnQuit;
        }

        public void OnDisable()
        {
            openMainMenuEvent.OnRaised -= OpenMainMenu;
            toggleHudEvent.OnRaised -= ToggleHud;
            openResultsPanelEvent.OnRaised -= OpenResultsPanel;
            pauseScenarioEvent.OnRaised -= OnPause;
            resumeScenarioEvent.OnRaised -= OnResume;
            quitEvent.OnRaised -= OnQuit;
        }

        public void Update()
        {
            if (!InputManager.IsMenuPressed() || IsInterfaceOpen)
            {
                return;
            }
            
            if (_paused)
            {
                resumeScenarioEvent.Raise();
            }
            else
            {
                pauseScenarioEvent.Raise();
            }
        }
        
        private void OpenMainMenu()
        {
            OnResume();
            mainMenu.SetActive(true);
        }

        private void ToggleHud(bool enable)
        {
            hud.SetActive(enable);
        }

        private void OpenResultsPanel()
        {
            Cursor.lockState = CursorLockMode.None; // TODO: if not controller
            resultsPanel.SetActive(true);
        }
        
        private void OnPause()
        {
            _paused = true;
            pauseMenu.SetActive(true);
        }

        private void OnResume()
        {
            _paused = false;
            pauseMenu.SetActive(false);
        }
        
        private static void OnQuit()
        {
            Application.Quit();
        }
        
        public static IEnumerator SelectButtonRoutine(GameObject button)
        {
            // Wait one frame due to Unity issue:
            // https://answers.unity.com/questions/1142958/buttonselect-doesnt-highlight.html?page=1&pageSize=5&sort=votes
            yield return null;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(button);
        }
    }
}