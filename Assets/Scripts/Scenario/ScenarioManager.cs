using Controller;
using Events;
using JetBrains.Annotations;
using UI;
using UnityEngine;

namespace Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        private const int ScenarioDurationS = 10;
        private const int CameraResetSpeed = 4;
        
        [SerializeField] private GameObject player;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private GameObject targetPrefab;
        
        [SerializeField] private PlayScenarioEvent playScenarioEvent;
        [SerializeField] private VoidEvent resumeScenarioEvent;
        [SerializeField] private VoidEvent pauseScenarioEvent;
        [SerializeField] private VoidEvent stopScenarioEvent;
        [SerializeField] private VoidEvent restartScenarioEvent;

        [SerializeField] private VoidEvent targetHitEvent;
        [SerializeField] private VoidEvent targetMissEvent;

        [SerializeField] private MainMenu mainMenu; // TODO: remove below references
        [SerializeField] private Hud hud;
        [SerializeField] private ResultsPanel resultsPanel;

        private StatsManager _statsManager;
        private Scenario _scenario;
        private bool _playing;
        private float _timer;

        public void OnEnable()
        {
            playScenarioEvent.OnRaised += OnPlay;
            pauseScenarioEvent.OnRaised += OnPause;
            resumeScenarioEvent.OnRaised += OnResume;
            stopScenarioEvent.OnRaised += OnStop;
            restartScenarioEvent.OnRaised += OnRestart;

            _statsManager = new StatsManager(hud, resultsPanel);
            targetHitEvent.OnRaised += _statsManager.OnTargetHit;
            targetMissEvent.OnRaised += _statsManager.OnTargetMiss;
        }

        public void OnDisable()
        {
            playScenarioEvent.OnRaised -= OnPlay;
            pauseScenarioEvent.OnRaised -= OnPause;
            resumeScenarioEvent.OnRaised -= OnResume;
            stopScenarioEvent.OnRaised -= OnStop;
            restartScenarioEvent.OnRaised -= OnRestart;
            targetHitEvent.OnRaised -= _statsManager.OnTargetHit;
            targetMissEvent.OnRaised -= _statsManager.OnTargetMiss;
        }
        
        public void Start()
        {
            var scenarios = GetComponents<Scenario>();
            mainMenu.CreateScenarioButtons(scenarios);
            
            foreach (var scenario in scenarios)
            {
                scenario.TargetPrefab = targetPrefab; // TODO: remove?
            }
        }

        private void OnPlay(Scenario scenario)
        {
            _scenario = scenario;
            _statsManager.Reset();
            hud.Toggle(true);

            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerController>().ResetCameras();
            player.SetActive(true);

            _timer = ScenarioDurationS;
            _scenario.StartScenario();
            _playing = true;
        }

        private void OnPause()
        {
            Time.timeScale = 0f;
            player.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
        }
        
        private void OnResume()
        {
            Time.timeScale = 1f;
            player.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnStop()
        {
            Time.timeScale = 1f;
            player.SetActive(false);
            hud.Toggle(false);
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
            Cursor.lockState = CursorLockMode.None;
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
                hud.SetTimer(secondsRemaining);
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