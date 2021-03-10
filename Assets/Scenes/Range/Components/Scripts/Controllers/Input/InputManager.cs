using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Range.Components.Scripts.Controllers.Input
{
    public static class InputManager
    {
        private static readonly PlayerInputActions InputActions = new PlayerInputActions();
        private static readonly string CameraAction = InputActions.Player.Camera.name;
        private static readonly string FireAction = InputActions.Player.Fire.name;
        private static readonly string AdsAction = InputActions.Player.Ads.name;
        private static readonly string ControllerControlScheme = InputActions.ControllerControlSchemeScheme.name;

        public static Vector2 GetRotationDelta(this PlayerInput input)
        {
            return input.actions[CameraAction].ReadValue<Vector2>();
        }
        
        public static bool IsFiring(this PlayerInput input)
        {
            return input.actions[FireAction].WasPressedThisFrame();
        }
        
        public static bool IsAds(this PlayerInput input)
        {
            return input.actions[AdsAction].IsPressed();
        }

        public static bool IsUsingController(this PlayerInput input)
        {
            return input.currentControlScheme.Equals(ControllerControlScheme);
        }
    }
}