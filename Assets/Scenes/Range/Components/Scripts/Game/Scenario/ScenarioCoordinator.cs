using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class ScenarioCoordinator : MonoBehaviour
    {
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private TrainingScenario scenario;

        public void Start()
        {
            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();
        }

        public void Update()
        {
            scenario.UpdateScenario();
        }

        public void FixedUpdate()
        {
            scenario.FixedUpdateScenario();
        }
    }
}