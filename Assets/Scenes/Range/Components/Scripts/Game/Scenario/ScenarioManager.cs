using Scenes.Range.Components.Scripts.Game.Event;
using Scenes.Range.Components.Scripts.Game.UI;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        [SerializeField] private Hud hud;
        [SerializeField] private Scenario scenario;
        [SerializeField] private GameObject targetPrefab;

        private const int DefaultDurationS = 30;
        private ScoreManager _scoreManager;
        private bool _playing;
        private float _timer;

        public void OnEnable()
        {
            EventBus.OnPlay += OnPlay;
        }
        
        public void OnDisable()
        {
            EventBus.OnPlay -= OnPlay;
        }

        private void OnPlay()
        {
            hud.Show();
            _scoreManager = new ScoreManager(hud);

            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();
            
            _timer = DefaultDurationS;
            _playing = true;
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
                scenario.EndScenario();
                _scoreManager.Reset();
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