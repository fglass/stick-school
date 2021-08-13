using Controller.Input;
using Game.Event;
using UnityEngine;

namespace Game.UI
{
    public class PauseMenu : MonoBehaviour
    {
        private Transform _pauseMenuCanvas;
        private Transform _mainMenuCanvas;
        private static bool _paused;
        
        public void Awake()
        {
            _pauseMenuCanvas = transform.Find("PauseMenu");
            _mainMenuCanvas = transform.Find("MainMenu");
        }

        public void Update()
        {
            if (!InputManager.IsMenuPressed() || IsMainMenuOpen)
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
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            EventBus.PublishResume();
        }
        
        private void Pause()
        {
            _paused = true;
            _pauseMenuCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            EventBus.PublishPause();
        }

        public void OnMenu()
        {
            _paused = false;
            _pauseMenuCanvas.gameObject.SetActive(false);
            _mainMenuCanvas.gameObject.SetActive(true);
            Time.timeScale = 1f;
            EventBus.PublishStop();
        }

        public void OnQuit()
        {
            Application.Quit();
        }

        private bool IsMainMenuOpen => _mainMenuCanvas.gameObject.activeSelf;
    }
}