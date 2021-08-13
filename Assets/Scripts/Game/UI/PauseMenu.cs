using Controller.Input;
using Game.Event;
using UnityEngine;

namespace Game.UI
{
    public class PauseMenu : MonoBehaviour
    {
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
            EventBus.PublishResume();
        }
        
        private void Pause()
        {
            _paused = true;
            _pauseMenuCanvas.gameObject.SetActive(true);
            EventBus.PublishPause();
        }

        public void OnMenu()
        {
            _paused = false;
            _pauseMenuCanvas.gameObject.SetActive(false);
            _mainMenuCanvas.gameObject.SetActive(true);
            EventBus.PublishStop();
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        private bool IsInterfaceOpen => _mainMenuCanvas.gameObject.activeSelf || _resultsPanelCanvas.gameObject.activeSelf; // TODO: rework
    }
}