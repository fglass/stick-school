using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Controller.Input
{
    public static class InputManager
    {
        private static readonly InputActions Actions = new InputActions();

        static InputManager()
        {
            InputUser.onChange += OnInputChange;
            Actions.Gameplay.Enable();
            Actions.UI.Enable();
        }

        public static Vector2 GetRotationDelta()
        {
            return Actions.Gameplay.Camera.ReadValue<Vector2>();
        }

        public static bool IsFiring()
        {
            return Actions.Gameplay.Fire.WasPressedThisFrame();
        }

        public static bool IsAds()
        {
            return Actions.Gameplay.Ads.IsPressed();
        }

        public static bool IsMenuPressed()
        {
            return Actions.UI.Menu.WasPressedThisFrame();
        }

        public static bool IsLeftMenuNavigationPressed() // TODO: event instead?
        {
            return Actions.UI.LeftMenuNavigation.WasPressedThisFrame();
        }

        public static bool IsRightMenuNavigationPressed()
        {
            return Actions.UI.RightMenuNavigation.WasPressedThisFrame();
        }

        public static bool IsUsingController()
        {
            return Gamepad.all.Count > 0; // TODO: replace with below
        }
        
        private static void OnInputChange(InputUser user, InputUserChange change, InputDevice _)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                Debug.Log(user.controlScheme == Actions.ControllerControlSchemeScheme); // TODO: use
            }
        }
    }
}