using Scenario;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Custom/DisplayResultsEvent")]
    public class DisplayResultsEvent : ScriptableObject
    {
        public UnityAction<ScoreHandler.ResultsDto> OnRaised { get; set; }

        public void Raise(ScoreHandler.ResultsDto results)
        {
            OnRaised?.Invoke(results);
        }
    }
}