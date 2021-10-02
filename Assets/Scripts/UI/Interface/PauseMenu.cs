using Events;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private VoidEvent openMainMenuEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;

        public void OnEnable()
        {
            openMainMenuEvent.OnRaised += OnMenu;
            restartScenarioEvent.OnRaised += Close;
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