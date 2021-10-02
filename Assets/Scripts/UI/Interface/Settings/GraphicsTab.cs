using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Interface.Settings
{
    public class GraphicsTab : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown displayModeDropdown;
        [SerializeField] private TMP_Dropdown displayFpsDropdown;
        [SerializeField] private TextMeshProUGUI framerateLimitText;
        [SerializeField] private Slider framerateLimitSlider;
        [SerializeField] private IntEvent setQualityEvent;
        [SerializeField] private IntEvent setResolutionEvent;
        [SerializeField] private IntEvent setDisplayModeEvent;
        [SerializeField] private FloatEvent setFramerateLimitEvent;

        private Resolution[] _resolutions;
        private readonly FullScreenMode[] _displayModes =
        {
            FullScreenMode.ExclusiveFullScreen, 
            FullScreenMode.FullScreenWindow, 
            FullScreenMode.Windowed
        };

        public void Awake()
        {
            InitialiseQualityDropdown();
            InitialiseResolutionDropdown();
            InitialiseDisplayModeDropdown();
            InitialiseDisplayFpsDropdown();
            InitialiseFramerateLimitSetting();
        }

        public void OnEnable()
        {
            setQualityEvent.OnRaised += SetQuality;
            setResolutionEvent.OnRaised += SetResolution;
            setDisplayModeEvent.OnRaised += SetDisplayMode;
            setFramerateLimitEvent.OnRaised += SetFramerateLimitText;
        }

        public void OnDisable()
        {
            setQualityEvent.OnRaised -= SetQuality;
            setResolutionEvent.OnRaised -= SetResolution;
            setDisplayModeEvent.OnRaised -= SetDisplayMode;
            setFramerateLimitEvent.OnRaised -= SetFramerateLimitText;
        }

        private void InitialiseQualityDropdown()
        {
            qualityDropdown.ClearOptions();
            qualityDropdown.AddOptions(QualitySettings.names.ToList());
            
            qualityDropdown.value = QualitySettings.GetQualityLevel();
            qualityDropdown.RefreshShownValue();
        }
        
        private void InitialiseResolutionDropdown()
        {
            _resolutions = Screen.resolutions;
            
            var resolutionOptions = new List<string>();
            var currentResolutionIndex = 0;
            
            for (var i = 0; i < _resolutions.Length; i++)
            {
                var resolution = _resolutions[i];
                resolutionOptions.Add($"{resolution.width} x {resolution.height}");
                
                if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }
            
            resolutionDropdown.ClearOptions();
            resolutionDropdown.AddOptions(resolutionOptions);

            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void InitialiseDisplayModeDropdown()
        {
            var options = new List<string> { "Fullscreen", "Fullscreen Windowed", "Windowed" };
            displayModeDropdown.ClearOptions();
            displayModeDropdown.AddOptions(options);

            displayModeDropdown.value = Array.IndexOf(_displayModes, Screen.fullScreenMode);
            displayModeDropdown.RefreshShownValue();
        }

        private void InitialiseDisplayFpsDropdown()
        {
            var options = new List<string> { "Enabled", "Disabled" };
            displayFpsDropdown.ClearOptions();
            displayFpsDropdown.AddOptions(options);

            displayFpsDropdown.value = 1;
            displayFpsDropdown.RefreshShownValue();
        }
        
        private void InitialiseFramerateLimitSetting()
        {
            SetFramerateLimitText(FramerateLimiter.Limit);
            framerateLimitSlider.value = FramerateLimiter.Limit;
        }

        private void SetResolution(int selectedIndex)
        {
            var selectedResolution = _resolutions[selectedIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }

        private static void SetQuality(int selectedIndex)
        {
            QualitySettings.SetQualityLevel(selectedIndex);
        }

        private void SetDisplayMode(int selectedIndex)
        {
            var selectedDisplayMode = _displayModes[selectedIndex];
            Screen.fullScreenMode = selectedDisplayMode;
        }
        
        private void SetFramerateLimitText(float limit)
        {
            framerateLimitText.text = Mathf.Round(limit).ToString(CultureInfo.InvariantCulture);
        }
    }
}