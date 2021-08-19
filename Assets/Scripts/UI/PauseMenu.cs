using Events;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OnMenu;
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