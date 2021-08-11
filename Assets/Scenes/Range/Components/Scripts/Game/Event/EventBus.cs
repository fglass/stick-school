
namespace Scenes.Range.Components.Scripts.Game.Event
{
    public static class EventBus
    {
        public delegate void GameAction();
        public static event GameAction OnPlay;
        public static event GameAction OnStop;
        public static event GameAction OnPause;
        public static event GameAction OnResume;
        public static event GameAction OnTargetHit;
        public static event GameAction OnTargetMiss;

        public static void PublishPlay()
        {
            OnPlay?.Invoke();
        }

        public static void PublishStop()
        {
            OnStop?.Invoke();
        }

        public static void PublishPause()
        {
            OnPause?.Invoke();
        }
        
        public static void PublishResume()
        {
            OnResume?.Invoke();
        }
        
        public static void PublishTargetHit()
        {
            OnTargetHit?.Invoke();
        }
        
        public static void PublishTargetMiss()
        {
            OnTargetMiss?.Invoke();
        }
    }
}