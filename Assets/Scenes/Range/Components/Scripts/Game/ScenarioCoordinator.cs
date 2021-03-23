using Scenes.Range.Components.Scripts.Game.Scenario;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class ScenarioCoordinator : MonoBehaviour
    {
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private TrainingScenario scenario;

        public void Start()
        {
            scenario.TargetPrefab = targetPrefab;
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