using Controller.Input;
using Events;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent quitEvent;

        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject resultsPanel;

        private bool _paused;

        private bool IsInterfaceOpen => mainMenu.activeSelf || resultsPanel.activeSelf;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OpenMainMenu;
            pauseScenarioEvent.OnRaised += OnPause;
            resumeScenarioEvent.OnRaised += OnResume;
            quitEvent.OnRaised += OnQuit;
        }

        public void OnDisable()
        {
            openMainMenuEvent.OnRaised -= OpenMainMenu;
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
            Debug.Log("quit");
            Application.Quit();
        }
    }
}