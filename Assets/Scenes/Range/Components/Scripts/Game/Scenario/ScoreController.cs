using Scenes.Range.Components.Scripts.Game.Event;
using Scenes.Range.Components.Scripts.Game.UI;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class ScoreController
    {
        private readonly CanvasController _canvasController;
        private int _score;
        private int _hitShots;
        private int _missedShots;
        
        public ScoreController(CanvasController canvasController)
        {
            _canvasController = canvasController;
            EventBus.OnTargetHit += OnTargetHit;
            EventBus.OnTargetMiss += OnTargetMiss;
        }

        private void OnTargetHit()
        {
            _hitShots++;
            _canvasController.SetAccuracy(CalculateAccuracy());
            _canvasController.SetScore(++_score);
        }

        private void OnTargetMiss()
        {
            _missedShots++;
            _canvasController.SetAccuracy(CalculateAccuracy());
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