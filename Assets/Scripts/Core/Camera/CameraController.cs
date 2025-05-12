using Core.Camera;
using Core.Game;
using DG.Tweening;
using Signals;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour,IGameStateChanged
{
    [SerializeField] private Transform _target;
    [SerializeField] private CameraView _view;


    private CameraModel _model;
    private SignalBus _signalBus;
    private Vector3 _currentOffset;
    private bool _isZoomed;
    private Tween _zoomTween;
    private bool _isActive;

    [Inject]
    private void Construct(CameraModel model, SignalBus signalBus)
    {
        _model = model;
        _signalBus = signalBus;

        _currentOffset = _model.Offset;

        _signalBus.Subscribe<BoostStateChangedSignal>(OnBoostChanged);
        _signalBus.Subscribe<GameStateChangedSignal>(OnGameStateChanged);
    }

    private void LateUpdate()
    {
        if (_target == null || !_isActive) return;
        
        Vector3 desired = _target.position + _currentOffset;
        Vector3 smooth = Vector3.Lerp(_view.transform.position, desired, Time.deltaTime * _model.FollowSpeed);

        _view.SetPosition(smooth);
    }

    private void OnBoostChanged(BoostStateChangedSignal signal)
    {
        if (signal.IsBoosting && !_isZoomed)
        {
            _isZoomed = true;
            ZoomIn();
        }
        else if (!signal.IsBoosting && _isZoomed)
        {
            _isZoomed = false;
            ZoomOut();
        }
        _view.BoostParticleState(_isZoomed);
    }

    private void ZoomIn()
    {
        _zoomTween?.Kill();
        _zoomTween = DOTween.To(() => _currentOffset.z, z => _currentOffset.z = z, 
            _model.Offset.z + _model.ZoomOffsetZ, _model.ZoomDuration);
    }

    private void ZoomOut()
    {
        _zoomTween?.Kill();
        _zoomTween = DOTween.To(() => _currentOffset.z, z => _currentOffset.z = z,
            _model.Offset.z, _model.ZoomDuration);
    }

    public void OnGameStateChanged(GameStateChangedSignal signal)
    {
        _isActive = signal.State != GameStateType.GameOver;

        if (signal.State == GameStateType.GameOver)
        {
            _view.CameraShake();
            _view.BoostParticleState(false);
        }
            
    }
}