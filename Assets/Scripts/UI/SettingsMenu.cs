using System;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private IntEvent setResolutionEvent;

        private Resolution[] resolutions;

        public void Awake()
        {
            InitialiseResolutionDropdown();
        }

        public void OnEnable()
        {
            setResolutionEvent.OnRaised += SetResolution;
        }

        public void OnDisable()
        {
            setResolutionEvent.OnRaised -= SetResolution;
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
    }
}