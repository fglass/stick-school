using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/VoidEvent")]
    public class VoidEvent : ScriptableObject
    {
        public UnityAction OnRaised { get; set; }

        public void Raise()
        {
            OnRaised?.Invoke();
        }
    }
}