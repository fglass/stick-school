using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/Custom/InitMainMenuEvent")]
    public class InitMainMenuEvent : ScriptableObject
    {
        public UnityAction<IReadOnlyList<Scenario.Scenario>> OnRaised { get; set; }

        public void Raise(IReadOnlyList<Scenario.Scenario> scenarios)
        {
            OnRaised?.Invoke(scenarios);
        }
    }
}