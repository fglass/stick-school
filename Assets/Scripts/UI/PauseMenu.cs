using Events;
using Input;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;
        [SerializeField] private GameObject resumeButton;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OnMenu;
            restartScenarioEvent.OnRaised += Close;
            
            if (InputManager.IsUsingController)
            {
                StartCoroutine(UIManager.SelectButtonRoutine(resumeButton));
            }
        }

        public void OnDisable()
        {
            openMainMenuEvent.OnRaised -= OnMenu;
            restartScenarioEvent.OnRaised -= Close;
        }

        private void OnMenu()
        {
            stopScenarioEvent.Raise();
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}