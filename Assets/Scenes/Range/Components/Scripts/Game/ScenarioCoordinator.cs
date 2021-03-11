using Scenes.Range.Components.Scripts.Game.Scenario;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game
{
    public class ScenarioCoordinator : MonoBehaviour
    {
        [SerializeField] private GameObject targetPrefab;
        private TrainingScenario _scenario;

        private void Start()
        {
            _scenario = gameObject.AddComponent<FlickScenario>();
            _scenario.TargetPrefab = targetPrefab;
        }
    }
}