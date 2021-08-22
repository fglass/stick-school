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

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OnMenu;
            
            if (InputManager.IsUsingController())
            {
                EventSystem.current.SetSelectedGameObject(transform.GetChild(1).gameObject); // TODO: to resume button
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