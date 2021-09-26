using System.Collections;
using Events;
using Input;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject fpsDisplay;
        
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private BoolEvent toggleHudEvent;
        [SerializeField] private VoidEvent openResultsPanelEvent;
        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;
        [SerializeField] private IntEvent setDisplayFpsEvent;
        [SerializeField] private VoidEvent quitEvent;

        private bool paused;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OpenMainMenu;
            toggleHudEvent.OnRaised += ToggleHud;
            openResultsPanelEvent.OnRaised += OpenResultsPanel;
            pauseScenarioEvent.OnRaised += OnPause;
            resumeScenarioEvent.OnRaised += OnResume;
            setDisplayFpsEvent.OnRaised += ToggleFps;
            quitEvent.OnRaised += OnQuit;
            ToggleCursor(true);
        }

        public void OnDisable()
        {
            openMainMenuEvent.OnRaised -= OpenMainMenu;
            toggleHudEvent.OnRaised -= ToggleHud;
            openResultsPanelEvent.OnRaised -= OpenResultsPanel;
            pauseScenarioEvent.OnRaised -= OnPause;
            resumeScenarioEvent.OnRaised -= OnResume;
            setDisplayFpsEvent.OnRaised -= ToggleFps;
            quitEvent.OnRaised -= OnQuit;
        }

        public void Update()
        {
            if (InputManager.IsMenuPressed())
            {
                OnMenuPressed();
            } else if (InputManager.IsRestartPressed())
            {
                OnRestartPressed();
            }
        }

        private void OnMenuPressed()
        {
            if (mainMenu.activeSelf || resultsPanel.activeSelf)
            {
                return;
            }
            
            if (paused)
            {
                resumeScenarioEvent.Raise();
            }
            else
            {
                pauseScenarioEvent.Raise();
            }
        }

        private void OnRestartPressed()
        {
            if (!mainMenu.activeSelf)
            {
                restartScenarioEvent.Raise();
            }
        }
        
        private void OpenMainMenu()
        {
            paused = false;
            pauseMenu.SetActive(false);
            mainMenu.SetActive(true);
            ToggleCursor(true);
        }

        private void ToggleHud(bool enable)
        {
            hud.SetActive(enable);
            if (enable)
            {
                ToggleCursor(false);
            }
        }

        private void OpenResultsPanel()
        {
            resultsPanel.SetActive(true);
            ToggleCursor(true);
        }
        
        private void OnPause()
        {
            paused = true;
            pauseMenu.SetActive(true);
            ToggleCursor(true);
        }

        private void OnResume()
        {
            paused = false;
            pauseMenu.SetActive(false);
            ToggleCursor(false);
        }
        
        private static void OnQuit()
        {
            EditorApplication.ExitPlaymode();
            Application.Quit();
        }
        
        private void ToggleFps(int active)
        {
            fpsDisplay.SetActive(active == 0);
        }

        private static void ToggleCursor(bool enable)
        {
            if (!enable)
            {
                Cursor.lockState = CursorLockMode.Locked;
            } else if (!InputManager.IsUsingController)
            {
                Cursor.lockState = CursorLockMode.None;
            }
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