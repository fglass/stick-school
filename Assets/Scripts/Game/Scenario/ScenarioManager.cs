using Game.Event;
using Game.UI;
using UnityEngine;

namespace Game.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Hud hud;
        [SerializeField] private ResultsModal resultsModal;
        [SerializeField] private Scenario scenario;
        [SerializeField] private GameObject targetPrefab;

        private const int DefaultDurationS = 15;
        private StatsManager _statsManager;
        private bool _playing;
        private float _timer;

        public void OnEnable()
        {
            _statsManager = new StatsManager(hud, resultsModal);
            EventBus.OnPlay += OnPlay;
            EventBus.OnPause += OnPause;
            EventBus.OnResume += OnResume;
            EventBus.OnStop += OnStop;
        }
        
        public void OnDisable()
        {
            EventBus.OnPlay -= OnPlay;
            EventBus.OnPause -= OnPause;
            EventBus.OnResume -= OnResume;
            EventBus.OnStop -= OnStop;
        }
        
        private void OnPlay()
        {
            _statsManager.Reset();
            hud.Toggle(true);
            player.SetActive(true);

            _timer = DefaultDurationS;
            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();

            _playing = true;
        }

        private void OnPause()
        {
            player.SetActive(false);
        }
        
        private void OnResume()
        {
            player.SetActive(true);
        }

        private void OnStop()
        {
            player.SetActive(false);
            scenario.EndScenario();
            hud.Toggle(false);
            _playing = false;
        }

        private void Finish()
        {
            OnStop();
            _statsManager.DisplayResults(scenario.Name);
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
            if (_playing)
            {
                scenario.FixedUpdateScenario();
            }
        }
    }
}