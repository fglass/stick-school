using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Custom/InitMainMenuEvent")]
    public class InitMainMenuEvent : ScriptableObject
    {
        public UnityAction<IEnumerable<Scenario.Scenario>> OnRaised { get; set; }

        public void Raise(IEnumerable<Scenario.Scenario> scenarios)
        {
            OnRaised?.Invoke(scenarios);
        }
    }
}