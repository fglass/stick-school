using Game.Event;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void OnPlay()
        {
            transform.Find("MainMenu").gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            EventBus.PublishPlay();
        }

        public void OnSettings()
        {
            Debug.Log("Settings");
        }

        public void OnQuit()
        {
            Application.Quit();
        }
    }
}