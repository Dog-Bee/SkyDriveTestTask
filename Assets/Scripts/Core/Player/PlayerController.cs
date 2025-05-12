using System;
using Core.Game;
using Infrastructure.Input;
using Infrastructure.Score;
using Signals;
using UnityEngine;
using Zenject;

namespace Core.Player
{
    public class PlayerController:MonoBehaviour,IGameStateChanged
    {
        [SerializeField] private PlayerView view;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float fallRayDistance = 5f;
        
        private IInputService _inputService;
        private SignalBus _signalBus;
        private PlayerModel _model;
        private DifficultyService _difficultyService;
        private float _xClamp = 5f;

        private bool _isActive;

        [Inject] 
        private void Construct(SignalBus signalBus, IInputService inputService,PlayerModel playerModel,DifficultyService difficultyService)
        {
            _signalBus = signalBus;
            _inputService = inputService;
            _model = playerModel;
            _difficultyService = difficultyService;
            
            _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
            _xClamp = _model.MaxX;
        }

        private void Update()
        {
            if(!_isActive) return;
            
            float inputX = _model.IsFalling? 0:_inputService.GetHorizontal();
            float speed = _model.CurrentSpeed(_difficultyService.GetSpeedMultiplier());
            float sideSpeed = _model.SideSpeed(_difficultyService.GetSpeedMultiplier());
            float downSpeed = _model.DownForce(_difficultyService.GetSpeedMultiplier());
            Vector3 newPosition = CalculatePlayerPosition(inputX, speed, sideSpeed,downSpeed);
            BoostHandle();
            CheckFall();
            HandleMovement(newPosition,inputX);
        }

        private void BoostHandle()
        {
            bool isNowBoosting = _inputService.IsBoostPressed();

            if (isNowBoosting != _model.IsBoosting)
            {
                _model.IsBoosting = isNowBoosting;
                _signalBus.Fire(new BoostStateChangedSignal{ IsBoosting = isNowBoosting });
            }
            
        }

        private Vector3 CalculatePlayerPosition(float inputX,float speed,float sideSpeed,float downSpeed)
        {
            Vector3 forwardMove = Vector3.forward *( speed * Time.deltaTime);
            Vector3 sideMove = Vector3.right *(inputX * sideSpeed * Time.deltaTime);
            Vector3 downMove = Vector3.down * (downSpeed * Time.deltaTime);
            Vector3 newPosition = playerTransform.position + forwardMove + sideMove + downMove;
            newPosition.x = Mathf.Clamp(newPosition.x, -_xClamp, _xClamp);
            return newPosition;
        }

        private void HandleMovement(Vector3 newPosition, float inputX)
        {
            playerTransform.position = newPosition;

            if (_model.IsFalling) return;
            
            if (MathF.Abs(inputX) > 0.01f)
            {
                view.SetTilt(inputX * _model.TiltAngle);
            }
            else
            {
                view.ResetTilt();
            }
            
        }

        private void CheckFall()
        {
            Vector3 origin = playerTransform.position;
            Vector3 direction = Vector3.down;

            if (!Physics.Raycast(origin, direction, fallRayDistance, layerMask) && !_model.IsFalling)
            {
                _model.IsFalling = true;
                view.SetFall(_model.FallAngle);
            }

            if (playerTransform.position.y < _model.MinY)
            {
                PlayerDie();
            }
        }

        private void PlayerDie()
        {
            view.OnPlayerDie();
            _signalBus.Fire<PlayerDiedSignal>(); 
        }

        public void OnGameStateChanged(GameStateChangedSignal signal)
        {
            _isActive = signal.State == GameStateType.Playing;
            view.TrailPlay(_isActive);
            if (signal.State == GameStateType.Menu)
                Reset();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Obstacle")) return;
            
            other.collider.enabled = false;
            PlayerDie();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!other.gameObject.CompareTag("ScoreZone")) return;
            
            other.enabled = false;
            _signalBus.Fire<AsteroidPassedSignal>();

        }

        public void Reset()
        {
            playerTransform.position = Vector3.zero;
            _model.IsFalling = false;
            view.Reset();
        }
    }
}