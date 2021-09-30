using Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class NavBarTab : MonoBehaviour
    {
        [SerializeField] private GameObject firstSelectableObject;

        public void OnEnable()
        {
            if (InputManager.IsUsingController && firstSelectableObject != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstSelectableObject);
            }
        }
    }
}