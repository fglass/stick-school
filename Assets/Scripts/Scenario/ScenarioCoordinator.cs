using Events;
using Player;
using UnityEngine;

namespace Scenario
{
    public class ScenarioCoordinator : MonoBehaviour
    {
        private const int CameraResetSpeed = 4;
        
        [SerializeField] private GameObject player;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private int scenarioDurationS = 30;
        [SerializeField] private int countdownDurationS = 3;

        [Header("Events")]
        [SerializeField] private InitMainMenuEvent initMainMenuEvent;
        [SerializeField] private BoolEvent toggleHudEvent;
        [SerializeField] private IntEvent setHudCountdownTimerEvent;
        [SerializeField] private IntEvent setHudTimerEvent;
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;
        
        private ScoreHandler _scoreHandler;
        private Scenario _scenario;
        private float _countdownTimer;
        private float _scenarioTimer;
        private bool _playing;

        public void Awake()
        {
            _scoreHandler = GetComponent<ScoreHandler>();
        }

        public void OnEnable()
        {
            playScenarioEvent.OnRaised += OnPlay;
            pauseScenarioEvent.OnRaised += OnPause;
            resumeScenarioEvent.OnRaised += OnResume;
            stopScenarioEvent.OnRaised += OnStop;
            restartScenarioEvent.OnRaised += OnRestart;
        }

        public void OnDisable()
        {
            playScenarioEvent.OnRaised -= OnPlay;
            pauseScenarioEvent.OnRaised -= OnPause;
            resumeScenarioEvent.OnRaised -= OnResume;
            stopScenarioEvent.OnRaised -= OnStop;
            restartScenarioEvent.OnRaised -= OnRestart;
        }
        
        public void Start()
        {
            var scenarios = GetComponents<Scenario>();
            initMainMenuEvent.Raise(scenarios);
        }

        private void OnPlay(Scenario scenario)
        {
            _scenario = scenario;
            _countdownTimer = countdownDurationS;
            Time.timeScale = 1f;

            toggleHudEvent.Raise(true);
            _scoreHandler.Reset();

            player.GetComponent<PlayerController>().Reset();
            player.SetActive(true);
            _playing = true;
        }
        
        private void OnPause()
        {
            Time.timeScale = 0f;
            player.SetActive(false);
        }
        
        private void OnResume()
        {
            Time.timeScale = 1f;
            player.SetActive(true);
        }

        private void OnStop()
        {
            player.SetActive(false);
            toggleHudEvent.Raise(false);
            _scenario.EndScenario();
            _playing = false;
            Time.timeScale = 1f;
        }
        
        private void OnRestart()
        {
            OnPlay(_scenario);
        }

        public void Update()
        {
            if (!_playing)
            {
                CenterCamera();
                return;
            }
            
            if (_countdownTimer > 0)
            {
                OnCountdownTick();
            }
            else
            {
                OnScenarioTick();
            }
        }

        public void FixedUpdate()
        {
            if (_playing && _countdownTimer <= 0)
            {
                _scenario.FixedUpdateScenario();
            }
        }
        
        private void OnCountdownTick()
        {
            _countdownTimer -= Time.deltaTime;

            var secondsRemaining = Mathf.CeilToInt(_countdownTimer);
            setHudCountdownTimerEvent.Raise(secondsRemaining);

            if (secondsRemaining <= 0)
            {
                StartScenario();
            }
        }
        
        private void StartScenario()
        {
            player.GetComponent<PlayerController>().CanFire(true);
            _scenarioTimer = scenarioDurationS;
            _scenario.StartScenario();
        }

        private void OnScenarioTick()
        {
            _scenarioTimer -= Time.deltaTime;

            if (_scenarioTimer <= 0)
            {
                FinishScenario();
            }
            else
            {
                var secondsRemaining = Mathf.CeilToInt(_scenarioTimer);
                setHudTimerEvent.Raise(secondsRemaining);
                _scenario.UpdateScenario();
            }
        }

        private void FinishScenario()
        {
            OnStop();
            _scoreHandler.DisplayResults(_scenario.Name);
        }
        
        private void CenterCamera()
        {
            if (Quaternion.Angle(cameraTransform.rotation, Quaternion.identity) > 0.01f)
            {
                cameraTransform.rotation =  Quaternion.Slerp(
                    cameraTransform.rotation, 
                    Quaternion.identity, 
                    CameraResetSpeed *  Time.deltaTime
                );
            }
        }
    }
}