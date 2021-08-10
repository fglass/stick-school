
namespace Scenes.Range.Components.Scripts.Game.Event
{
    public static class EventManager
    {
        public delegate void TargetHitAction();
        public static event TargetHitAction OnTargetHit;

        public static void OnHitTarget()
        {
            OnTargetHit?.Invoke();
        }
    }
}