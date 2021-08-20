using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/BoolEvent")]
    public class BoolEvent : ScriptableObject
    {
        public UnityAction<bool> OnRaised { get; set; }

        public void Raise(bool value)
        {
            OnRaised?.Invoke(value);
        }
    }
}