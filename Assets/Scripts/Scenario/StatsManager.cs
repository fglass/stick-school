using Events;
using Game.UI;
using UI;

namespace Scenario
{
    public class StatsManager
    {
        private readonly Hud _hud;
        private readonly ResultsPanel _resultsPanel;
        
        private int _score;
        private int _hitShots;
        private int _missedShots;
        
        public StatsManager(Hud hud, ResultsPanel resultsPanel)
        {
            _hud = hud;
            _resultsPanel = resultsPanel;
        }

        public void OnTargetHit()
        {
            _score += 100;
            _hitShots++;
            
            _hud.SetScore(_score);
            _hud.SetAccuracy(CalculateAccuracy());
        }

        public void OnTargetMiss()
        {
            _missedShots++;
            _hud.SetAccuracy(CalculateAccuracy());
        }

        private int CalculateAccuracy()
        {
            float totalShots = _hitShots + _missedShots;

            if (totalShots <= 0)
            {
                return 0;
            }
            
            var accuracy = _hitShots / totalShots * 100;
            return (int) accuracy;
        }

        public void DisplayResults(string scenarioName) 
        {
            _resultsPanel.Display(scenarioName, _score, _hitShots, _missedShots, CalculateAccuracy()); // TODO: DTO
        }

        public void Reset()
        {
            _score = 0;
            _hitShots = 0;
            _missedShots = 0;
            _hud.SetScore(0);
            _hud.SetAccuracy(0);
        }
    }
}