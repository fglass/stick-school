using Events;
using Input;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        [SerializeField] private GameObject resumeButton;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OnMenu;
            
            if (InputManager.IsUsingController)
            {
                StartCoroutine(UIManager.SelectButtonRoutine(resumeButton));
            }
        }

        public void OnDisable()
        {
            openMainMenuEvent.OnRaised -= OnMenu;
        }

        private void OnMenu()
        {
            stopScenarioEvent.Raise();
        }
    }
}