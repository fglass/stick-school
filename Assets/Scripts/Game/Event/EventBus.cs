using JetBrains.Annotations;

namespace Game.Event
{
    public static class EventBus
    {
        public delegate void PlayAction([CanBeNull] Scenario.Scenario scenario);
        public static event PlayAction OnPlay;

        public delegate void GameAction();
        public static event GameAction OnStop;
        public static event GameAction OnPause;
        public static event GameAction OnResume;
        public static event GameAction OnTargetHit;
        public static event GameAction OnTargetMiss;

        public static void PublishPlay([CanBeNull] Scenario.Scenario scenario = null) => OnPlay?.Invoke(scenario);
        public static void PublishStop() => OnStop?.Invoke();
        public static void PublishPause() => OnPause?.Invoke();
        public static void PublishResume() => OnResume?.Invoke();
        public static void PublishTargetHit() => OnTargetHit?.Invoke();
        public static void PublishTargetMiss() => OnTargetMiss?.Invoke();
    }
}