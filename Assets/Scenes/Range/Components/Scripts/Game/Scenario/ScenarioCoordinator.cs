using Scenes.Range.Components.Scripts.Game.UI;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class ScenarioCoordinator : MonoBehaviour
    {
        [SerializeField] private CanvasController canvasController;
        [SerializeField] private TrainingScenario scenario;
        [SerializeField] private GameObject targetPrefab;

        private float _timer = 10;

        public void Start()
        {
            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();
        }

        public void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                scenario.EndScenario();
            }
            else
            {
                var secondsRemaining = (int) _timer;
                canvasController.SetTimer(secondsRemaining);
                scenario.UpdateScenario();
            }
        }

        public void FixedUpdate()
        {
            scenario.FixedUpdateScenario();
        }
    }
}