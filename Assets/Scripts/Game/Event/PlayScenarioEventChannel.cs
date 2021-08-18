using UnityEngine;
using UnityEngine.Events;

namespace Game.Event
{
    [CreateAssetMenu]
    public class PlayScenarioEventChannel : ScriptableObject
    {
        public UnityAction<Scenario.Scenario> Event { get; set; }
        
        public void RaiseEvent(Scenario.Scenario scenario)
        {
            Event?.Invoke(scenario);
        }
    }
}