using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Input
{
    public static class InputManager
    {
        private static readonly InputActions Actions = new InputActions();
        public delegate void InputChangeEventHandler(bool isUsingController);
        public static event InputChangeEventHandler InputChangeEvent;

        public static bool IsUsingController { get; private set; }

        static InputManager()
        {
            Actions.Gameplay.Enable();
            Actions.UI.Enable();
            IsUsingController = Gamepad.all.Count > 0;
            InputUser.onChange += OnInputChange;
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
            return Actions.Gameplay.Menu.WasPressedThisFrame();
        }
        
        public static bool IsRestartPressed()
        {
            return Actions.Gameplay.Restart.WasPressedThisFrame();
        }

        public static bool IsLeftMenuNavigationPressed() // TODO: event instead?
        {
            return Actions.UI.LeftMenuNavigation.WasPressedThisFrame();
        }

        public static bool IsRightMenuNavigationPressed()
        {
            return Actions.UI.RightMenuNavigation.WasPressedThisFrame();
        }
        
        private static void OnInputChange(InputUser user, InputUserChange change, InputDevice device)
        {
            if (change == InputUserChange.ControlSchemeChanged)
            {
                IsUsingController = user.controlScheme == Actions.ControllerControlSchemeScheme;
                InputChangeEvent?.Invoke(IsUsingController);
            }
        }
    }
}