using Game.Event;
using UnityEngine;

namespace Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        private GameObject _mainMenu;

        public void Awake()
        {
            _mainMenu = transform.Find("MainMenu").gameObject;
        }

        public void OnPlay()
        {
            _mainMenu.SetActive(false);
            EventBus.PublishPlay();
        }

        public void OnSettings()
        {
            Debug.Log("Open settings");
        }

        public void OnQuit()
        {
            Application.Quit();
        }
    }
}