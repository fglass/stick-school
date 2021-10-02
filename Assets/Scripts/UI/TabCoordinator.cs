using Events;
using TMPro;
using UnityEngine;

namespace UI
{
    public class TabCoordinator : MonoBehaviour
    {
        [SerializeField] private GameObject[] tabs;
        [SerializeField] private TextMeshProUGUI[] tabTexts;
        [SerializeField] private IntEvent selectTabEvent;

        private int _selectedTabIndex;

        public void OnEnable()
        {
            selectTabEvent.OnRaised += SelectTab;
            Reselect();
        }
        
        public void OnDisable()
        {
            selectTabEvent.OnRaised -= SelectTab;
        }

        public GameObject GetTab(int index)
        {
            return tabs[index];
        }

        public void SelectTab(int index)
        {
            DeselectTabs();
            _selectedTabIndex = index;
            tabTexts[_selectedTabIndex].GetComponent<TextInteraction>().Select();

            var selectedTab = tabs[_selectedTabIndex];
            selectedTab.SetActive(true);
        }

        private void DeselectTabs()
        {
            for (var i = 0; i < tabs.Length; i++)
            {
                tabs[i].SetActive(false);
                tabTexts[i].GetComponent<TextInteraction>().Deselect();
            }
        }
        
        public void Reselect()
        {
            SelectTab(_selectedTabIndex);
        }

        public void SelectNextTab()
        {
            var nextTabIndex = Mod(_selectedTabIndex + 1, tabs.Length);
            SelectTab(nextTabIndex);
            
        }

        public void SelectPreviousTab()
        {
            var previousTabIndex = Mod(_selectedTabIndex - 1, tabs.Length);
            SelectTab(previousTabIndex);
        }

        private static int Mod(int n, int m)
        {
            return (n %= m) < 0 ? n + m : n;
        }
    }
}