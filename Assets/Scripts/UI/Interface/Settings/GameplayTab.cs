using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI.Interface.Settings
{
    public class GameplayTab : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown gameDropdown;
        [SerializeField] private TMP_Dropdown toggleCrosshairDropdown;
        [SerializeField] private TMP_Dropdown toggleWeaponDropdown;

        public void Awake()
        {
            InitialiseGameDropdown();
            InitialiseToggles();
        }
        
        private void InitialiseGameDropdown()
        {
            var options = new List<string> { "Apex Legends", "Call of Duty: Warzone" }; // TODO: dynamic
            gameDropdown.ClearOptions();
            gameDropdown.AddOptions(options);
        }
        
        private void InitialiseToggles()
        {
            var toggles = new[] { toggleCrosshairDropdown, toggleWeaponDropdown };
            var options = new List<string> { "Enabled", "Disabled" };

            foreach (var toggle in toggles)
            {
                toggle.ClearOptions();
                toggle.AddOptions(options);
            }
        }
    }
}