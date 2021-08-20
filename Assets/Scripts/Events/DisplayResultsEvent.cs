using Scenario;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Custom/DisplayResultsEvent")]
    public class DisplayResultsEvent : ScriptableObject
    {
        public UnityAction<StatsManager.ResultsDto> OnRaised { get; set; }

        public void Raise(StatsManager.ResultsDto results)
        {
            OnRaised?.Invoke(results);
        }
    }
}