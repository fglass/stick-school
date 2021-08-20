using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/IntEvent")]
    public class IntEvent : ScriptableObject
    {
        public UnityAction<int> OnRaised { get; set; }

        public void Raise(int value)
        {
            OnRaised?.Invoke(value);
        }
    }
}