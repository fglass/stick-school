using System;
using System.Collections;
using Input;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ControllerSelection : MonoBehaviour
    {
        [SerializeField] private GameObject firstObject;

        public void OnEnable()
        {
            InputManager.InputChangeEvent += OnInputChange;

            if (InputManager.IsUsingController)
            {
                SelectObject();
            }
        }

        public void OnDisable()
        {
            InputManager.InputChangeEvent -= OnInputChange;
        }

        private void OnInputChange(bool isUsingController)
        {
            if (isUsingController)
            {
                SelectObject();
            }
        }

        private void SelectObject()
        {
            StartCoroutine(SelectObject(firstObject));
        }
        
        private static IEnumerator SelectObject(GameObject @object)
        {
            // Wait one frame due to Unity issue:
            // https://answers.unity.com/questions/1142958/buttonselect-doesnt-highlight.html?page=1&pageSize=5&sort=votes
            yield return null;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(@object);
        }
    }
}