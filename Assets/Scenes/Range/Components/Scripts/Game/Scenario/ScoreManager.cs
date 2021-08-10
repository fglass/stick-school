using Scenes.Range.Components.Scripts.Game.Event;
using Scenes.Range.Components.Scripts.Game.UI;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class ScoreManager
    {
        private readonly Hud _hud;
        private int _score;
        private int _hitShots;
        private int _missedShots;
        
        public ScoreManager(Hud hud)
        {
            _hud = hud;
            EventBus.OnTargetHit += OnTargetHit;
            EventBus.OnTargetMiss += OnTargetMiss;
        }

        private void OnTargetHit()
        {
            _hitShots++;
            _hud.SetAccuracy(CalculateAccuracy());
            _hud.SetScore(++_score);
        }

        private void OnTargetMiss()
        {
            _missedShots++;
            _hud.SetAccuracy(CalculateAccuracy());
        }

        private int CalculateAccuracy()
        {
            float totalShots = _hitShots + _missedShots;
            var accuracy = _hitShots / totalShots * 100;
            return (int) accuracy;
        }

        public void Reset()
        {
            _score = 0;
            _hitShots = 0;
            _missedShots = 0;
        }
    }
}