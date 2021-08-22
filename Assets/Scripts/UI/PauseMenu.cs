using System.Collections;
using Controller.Input;
using Events;
using UnityEngine;
using UnityEngine.EventSystems;

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
            
            if (InputManager.IsUsingController())
            {
                StartCoroutine(SelectResumeButton());
            }
        }
        
        private IEnumerator SelectResumeButton()
        {
            // Wait one frame due to Unity issue: https://answers.unity.com/questions/1142958/buttonselect-doesnt-highlight.html?page=1&pageSize=5&sort=votes
            yield return null;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);
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