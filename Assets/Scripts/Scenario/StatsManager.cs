using Events;
using UnityEngine;

namespace Scenario
{
    public class StatsManager : MonoBehaviour
    {
        [SerializeField] private VoidEvent targetHitEvent;
        [SerializeField] private VoidEvent targetMissEvent;
        [SerializeField] private IntEvent setHudScoreEvent;
        [SerializeField] private IntEvent setHudAccuracyEvent;
        [SerializeField] private VoidEvent openResultsPanelEvent;
        [SerializeField] private DisplayResultsEvent displayResultsEvent;

        private int _score;
        private int _hitShots;
        private int _missedShots;

        public void OnEnable()
        {
            targetHitEvent.OnRaised += OnTargetHit;
            targetMissEvent.OnRaised += OnTargetMiss;
        }
        
        public void OnDisable()
        {
            targetHitEvent.OnRaised -= OnTargetHit;
            targetMissEvent.OnRaised -= OnTargetMiss;
        }
        
        private void OnTargetHit()
        {
            _score += 100;
            _hitShots++;
            
            setHudScoreEvent.Raise(_score);
            setHudAccuracyEvent.Raise(CalculateAccuracy());
        }

        private void OnTargetMiss()
        {
            _missedShots++;
            setHudAccuracyEvent.Raise(CalculateAccuracy());
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
            var results = new ResultsDto(scenarioName, _score, _hitShots, _missedShots, CalculateAccuracy());
            openResultsPanelEvent.Raise();
            displayResultsEvent.Raise(results);
        }

        public void Reset()
        {
            _score = 0;
            _hitShots = 0;
            _missedShots = 0;
            setHudScoreEvent.Raise(0);
            setHudAccuracyEvent.Raise(0);
        }

        public readonly struct ResultsDto
        {
            public ResultsDto(string scenarioName, int score, int hitShots, int missedShots, int accuracy)
            {
                ScenarioName = scenarioName;
                Score = score;
                HitShots = hitShots;
                MissedShots = missedShots;
                Accuracy = accuracy;
            }
            
            public string ScenarioName { get; }
            public int Score { get; }
            public int HitShots { get; }
            public int MissedShots { get; }
            public int Accuracy { get; }
        }
    }
}