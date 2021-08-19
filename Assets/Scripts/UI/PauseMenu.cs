using Controller.Input;
using Events;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        
        private Transform _pauseMenuCanvas;
        private Transform _mainMenuCanvas;
        private Transform _resultsPanelCanvas;
        private static bool _paused;
        
        public void Awake()
        {
            _pauseMenuCanvas = transform.Find("PauseMenu");
            _mainMenuCanvas = transform.Find("MainMenu");
            _resultsPanelCanvas = transform.Find("ResultsPanel");
        }

        public void Update()
        {
            if (!InputManager.IsMenuPressed() || IsInterfaceOpen)
            {
                return;
            }
            
            if (_paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        public void Resume()
        {
            _paused = false;
            _pauseMenuCanvas.gameObject.SetActive(false);
            resumeScenarioEvent.Raise();
        }
        
        private void Pause()
        {
            _paused = true;
            _pauseMenuCanvas.gameObject.SetActive(true);
            pauseScenarioEvent.Raise();
        }

        public void OnMenu()
        {
            _paused = false;
            _pauseMenuCanvas.gameObject.SetActive(false);
            _mainMenuCanvas.gameObject.SetActive(true);
            stopScenarioEvent.Raise();
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        private bool IsInterfaceOpen => _mainMenuCanvas.gameObject.activeSelf || _resultsPanelCanvas.gameObject.activeSelf; // TODO: rework
    }
}