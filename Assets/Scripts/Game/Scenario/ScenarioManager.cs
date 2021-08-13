using Controller;
using Game.Event;
using Game.UI;
using UnityEngine;

namespace Game.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        private const int ScenarioDurationS = 5;
        private const int CameraResetSpeed = 4;
        
        [SerializeField] private GameObject player;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Hud hud;
        [SerializeField] private ResultsPanel resultsPanel;
        [SerializeField] private Scenario scenario;
        [SerializeField] private GameObject targetPrefab;
        
        private StatsManager _statsManager;
        private bool _playing;
        private float _timer;

        public void OnEnable()
        {
            _statsManager = new StatsManager(hud, resultsPanel);
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

            Cursor.lockState = CursorLockMode.Locked;
            player.GetComponent<PlayerController>().ResetCameras();
            player.SetActive(true);

            _timer = ScenarioDurationS;
            scenario.TargetPrefab = targetPrefab;
            scenario.StartScenario();

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
            scenario.EndScenario();
            _playing = false;
        }

        private void Finish()
        {
            OnStop();
            Cursor.lockState = CursorLockMode.None;
            _statsManager.DisplayResults(scenario.Name);
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