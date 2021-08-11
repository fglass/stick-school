using Game.Event;
using Game.UI;
using UnityEngine;

namespace Game.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] private Hud hud;
        [SerializeField] private ResultsModal resultsModal;
        [SerializeField] private Scenario scenario;
        [SerializeField] private GameObject targetPrefab;

        private const int DefaultDurationS = 15;
        private ScoreManager _scoreManager;
        private bool _playing;
        private float _timer;

        public void OnEnable()
        {
            _scoreManager = new ScoreManager(hud, resultsModal);
            EventBus.OnPlay += OnPlay;
            EventBus.OnStop += OnStop;
        }
        
        public void OnDisable()
        {
            EventBus.OnPlay -= OnPlay;
            EventBus.OnStop -= OnStop;
        }
        
        private void OnPlay()
        {
            hud.Toggle(true);
            _scoreManager.Reset();

            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();
            
            _timer = DefaultDurationS;
            _playing = true;
        }

        private void OnStop()
        {
            scenario.EndScenario();
            hud.Toggle(false);
            _playing = false;
        }

        private void Finish()
        {
            OnStop();
            _scoreManager.DisplayResults(scenario.Name);
        }

        public void Update()
        {
            if (!_playing)
            {
                return;
            }
            
            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                Finish();
            }
            else
            {
                var secondsRemaining = (int) _timer;
                hud.SetTimer(secondsRemaining);
                scenario.UpdateScenario();
            }
        }

        public void FixedUpdate()
        {
            scenario.FixedUpdateScenario();
        }
    }
}