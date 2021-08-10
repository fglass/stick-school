using Scenes.Range.Components.Scripts.Game.Event;
using UnityEngine;

namespace Scenes.Range.Components.Scripts.Game.UI
{
    public class MainMenu : MonoBehaviour
    {
        public void Play()
        {
            gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            EventBus.PublishPlay();
        }
    }
}