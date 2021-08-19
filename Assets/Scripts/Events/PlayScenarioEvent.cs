using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/PlayScenarioEvent")]
    public class PlayScenarioEvent : ScriptableObject
    {
        public UnityAction<Scenario.Scenario> OnRaised { get; set; }

        public void Raise(Scenario.Scenario scenario = null)
        {
            OnRaised?.Invoke(scenario);
        }
    }
}