using Controller;
using Events;
using UnityEngine;

namespace Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        private const int CameraResetSpeed = 4;
        
        [SerializeField] private GameObject player;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private GameObject targetPrefab;
        [SerializeField] private int scenarioDurationS = 30;

        [Header("Events")]
        [SerializeField] private InitMainMenuEvent initMainMenuEvent;
        [SerializeField] private BoolEvent toggleHudEvent;
        [SerializeField] private IntEvent setHudTimerEvent;
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;
        
        private StatsManager _statsManager;
        private Scenario _scenario;
        private bool _playing;
        private float _timer;

        public void Awake()
        {
            _statsManager = GetComponent<StatsManager>();
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
            
            foreach (var scenario in scenarios)
            {
                scenario.TargetPrefab = targetPrefab; // TODO: remove?
            }
        }

        private void OnPlay(Scenario scenario)
        {
            toggleHudEvent.Raise(true);
            _statsManager.Reset();
            _timer = scenarioDurationS;

            Time.timeScale = 1f;

            player.GetComponent<PlayerController>().ResetCameras();
            player.SetActive(true);

            _scenario = scenario;
            _scenario.StartScenario();
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
            Time.timeScale = 1f;
            player.SetActive(false);
            toggleHudEvent.Raise(false);
            _scenario.EndScenario();
            _playing = false;
        }
        
        private void OnRestart()
        {
            OnPlay(_scenario);
        }

        private void Finish()
        {
            OnStop();
            _statsManager.DisplayResults(_scenario.Name);
        }

        public void Update()
        {
            if (!_playing)
            {
                CenterCamera();
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
                setHudTimerEvent.Raise(secondsRemaining);
                _scenario.UpdateScenario();
            }
        }

        public void FixedUpdate()
        {
            if (_playing)
            {
                _scenario.FixedUpdateScenario();
            }
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