using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    [CreateAssetMenu(menuName = "Events/FloatEvent")]
    public class FloatEvent : ScriptableObject
    {
        public UnityAction<float> OnRaised { get; set; }

        public void Raise(float value)
        {
            OnRaised?.Invoke(value);
        }
    }
}