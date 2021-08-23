using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller.Input
{
    public static class InputManager
    {
        private static readonly InputActions Actions = new InputActions();

        static InputManager()
        {
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
        
        public static bool IsLeftMenuNavigationPressed()
        {
            return Actions.UI.LeftMenuNavigation.WasPressedThisFrame();
        }
        
        public static bool IsRightMenuNavigationPressed()
        {
            return Actions.UI.RightMenuNavigation.WasPressedThisFrame();
        }

        public static bool IsUsingController()
        {
            return Gamepad.all.Count > 0; // TODO: last action or setting - Gamepad.current.lastUpdateTime
        }
    }
}