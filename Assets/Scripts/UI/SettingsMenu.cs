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
        [SerializeField] private IntEvent setQualityEvent;
        [SerializeField] private IntEvent setResolutionEvent;

        private Resolution[] resolutions;

        public void Awake()
        {
            InitialiseQualityDropdown();
            InitialiseResolutionDropdown();
        }

        public void OnEnable()
        {
            setQualityEvent.OnRaised += SetQuality;
            setResolutionEvent.OnRaised += SetResolution;
        }

        public void OnDisable()
        {
            setQualityEvent.OnRaised -= SetQuality;
            setResolutionEvent.OnRaised -= SetResolution;
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

        private void SetResolution(int selectedIndex)
        {
            var selectedResolution = resolutions[selectedIndex];
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        }

        private static void SetQuality(int selectedIndex)
        {
            QualitySettings.SetQualityLevel(selectedIndex);
        }
    }
}