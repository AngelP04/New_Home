using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace GameInput
{

    public enum mouseButton
    {
        Left,
        Right
    }
    public class MouseUser : MonoBehaviour
    {
        private InputActions _inputActions;

        public Vector2 MousePosition { get; private set; }
        public Vector2 MouseInWorldPosition => (Vector2)Camera.main.ScreenToWorldPoint(MousePosition);

        private bool _isLeftMouseButtonPressed;
        private bool _isRightMouseButtonPressed;

        private void OnEnable()
        {
            _inputActions = InputActions.Instance;
            _inputActions.Player.MousePosition.performed += OnMousePositionPerformance;
            _inputActions.Player.Performaction.performed += OnPerformanceActionPerfomance;
            _inputActions.Player.Performaction.canceled += OnPerformanceActionCancelled;
            _inputActions.Player.Performaction.performed += OnCancelActionPerfomance;
            _inputActions.Player.Performaction.canceled += OnCancelActionCancelled;
        }

        private void OnDisable()
        {
            _inputActions.Player.MousePosition.performed -= OnMousePositionPerformance;
            _inputActions.Player.Performaction.performed -= OnPerformanceActionPerfomance;
            _inputActions.Player.Performaction.canceled -= OnPerformanceActionCancelled;
            _inputActions.Player.Performaction.performed -= OnCancelActionPerfomance;
            _inputActions.Player.Performaction.canceled -= OnCancelActionCancelled;
        }

        private void OnMousePositionPerformance(InputAction.CallbackContext ctx)
        {
            MousePosition = ctx.ReadValue<Vector2>();
        }

        private void OnPerformanceActionPerfomance(InputAction.CallbackContext ctx)
        {
            _isRightMouseButtonPressed = true;
        }

        private void OnPerformanceActionCancelled(InputAction.CallbackContext ctx)
        {
            _isRightMouseButtonPressed = false;
        }

        private void OnCancelActionPerfomance(InputAction.CallbackContext ctx)
        {
            _isLeftMouseButtonPressed = true;
        }

        private void OnCancelActionCancelled(InputAction.CallbackContext ctx)
        {
            _isLeftMouseButtonPressed = false;
        }

        public bool IsMouseButtonPressed(mouseButton button)
        {
            return button == mouseButton.Left ? _isLeftMouseButtonPressed : _isRightMouseButtonPressed;
        }
    }
}