using Game.Event;
using Game.UI;

namespace Game.Scenario
{
    public class ScoreManager
    {
        private readonly Hud _hud;
        private readonly ResultsModal _resultsModal;
        private int _score;
        private int _hitShots;
        private int _missedShots;
        
        public ScoreManager(Hud hud, ResultsModal resultsModal)
        {
            _hud = hud;
            _resultsModal = resultsModal;
            EventBus.OnTargetHit += OnTargetHit;
            EventBus.OnTargetMiss += OnTargetMiss;
        }

        private void OnTargetHit()
        {
            _hitShots++;
            _score += 100;
            
            _hud.SetAccuracy(CalculateAccuracy());
            _hud.SetScore(_score);
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

        public void DisplayResults(string scenarioName)
        {
            _resultsModal.Display(scenarioName, _score, _hitShots, _missedShots, CalculateAccuracy());
        }

        public void Reset()
        {
            _score = 0;
            _hitShots = 0;
            _missedShots = 0;
        }
    }
}