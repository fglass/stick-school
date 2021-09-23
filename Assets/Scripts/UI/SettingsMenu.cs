using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown qualityDropdown;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown displayModeDropdown;
        [SerializeField] private IntEvent setQualityEvent;
        [SerializeField] private IntEvent setResolutionEvent;
        [SerializeField] private IntEvent setDisplayModeEvent;

        private Resolution[] resolutions;
        private readonly FullScreenMode[] displayModes =
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
        }

        public void OnEnable()
        {
            setQualityEvent.OnRaised += SetQuality;
            setResolutionEvent.OnRaised += SetResolution;
            setDisplayModeEvent.OnRaised += SetDisplayMode;
        }

        public void OnDisable()
        {
            setQualityEvent.OnRaised -= SetQuality;
            setResolutionEvent.OnRaised -= SetResolution;
            setDisplayModeEvent.OnRaised -= SetDisplayMode;
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
            resolutions = Screen.resolutions;
            
            var resolutionOptions = new List<string>();
            var currentResolutionIndex = 0;
            
            for (var i = 0; i < resolutions.Length; i++)
            {
                var resolution = resolutions[i];
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
            var options = new List<string>() { "Fullscreen", "Fullscreen Windowed", "Windowed" };
            displayModeDropdown.ClearOptions();
            displayModeDropdown.AddOptions(options);

            displayModeDropdown.value = Array.IndexOf(displayModes, Screen.fullScreenMode);
            displayModeDropdown.RefreshShownValue();
        }

        private void SetResolution(int selectedIndex)
        {
            var selectedResolution = resolutions[selectedIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }

        private static void SetQuality(int selectedIndex)
        {
            QualitySettings.SetQualityLevel(selectedIndex);
        }

        private void SetDisplayMode(int selectedIndex)
        {
            var selectedDisplayMode = displayModes[selectedIndex];
            Screen.fullScreenMode = selectedDisplayMode;
        }
    }
}