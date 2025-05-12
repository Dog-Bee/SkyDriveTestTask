using UnityEngine;


namespace Infrastructure.Input
{
    public class InputService:IInputService
    {
        private Controls _controls;
        private float _sideMove;
        private bool _isBoosting;
        

        public InputService()
        {
            _controls = new Controls();
            _controls.Gameplay.Enable();
            _controls.Gameplay.Move.performed += ctx => _sideMove = ctx.ReadValue<float>();
            _controls.Gameplay.Move.canceled += _ => _sideMove = 0;
            
            _controls.Gameplay.Boost.started += _ => _isBoosting = true;
            _controls.Gameplay.Boost.canceled += _ => _isBoosting = false;
            
        }
        public float GetHorizontal()
        {
            return _sideMove;
        }

        public bool IsBoostPressed()
        {
            return _isBoosting;
        }

        public bool IsAnyKeyPressed()
        {
            return _controls.Gameplay.AnyKey.IsPressed();
        }
    }
}