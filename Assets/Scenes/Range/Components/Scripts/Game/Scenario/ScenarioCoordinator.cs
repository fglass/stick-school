using Scenes.Range.Components.Scripts.Game.UI;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class ScenarioCoordinator : MonoBehaviour
    {
        [SerializeField] private CanvasController canvasController;
        [SerializeField] private Scenario scenario;
        [SerializeField] private GameObject targetPrefab;

        private ScoreController _scoreController;
        private float _timer = 30;

        public void Start()
        {
            _scoreController = new ScoreController(canvasController);
            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();
        }

        public void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                scenario.EndScenario();
                _scoreController.Reset();
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