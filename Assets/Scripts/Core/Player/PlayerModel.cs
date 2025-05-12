namespace Core.Player
{
    public class PlayerModel
    {
        private readonly float _baseSpeed = 10f;
        private readonly float _boostMultiplier = 2f;
        private readonly float _tiltAngle = 25f;
        private readonly float _fallAngle = 45f;
        private readonly float _lateralSpeed = 5f;
        private readonly float _downSpeed = 15f;
        private readonly float _minY = 5f;
        private readonly float _maxX = 20;
        
        public float TiltAngle => _tiltAngle;
        public float FallAngle => _fallAngle;
        public float MinY => _minY;
        public float MaxX => _maxX;

        public PlayerModel(PlayerConfig config)
        {
            _baseSpeed = config.BaseSpeed;
            _boostMultiplier = config.BoostMultiplier;
            _tiltAngle = config.TiltAngle;
            _lateralSpeed = config.LateralSpeed;
            _maxX = config.MaxX;
            _minY = config.MinY;
            _downSpeed = config.DownSpeed;
            _fallAngle = config.FallAngle;
        }
        
        public float CurrentSpeed(float difficultyMultiplier)
        {
            return IsBoosting ? _boostMultiplier * _baseSpeed*difficultyMultiplier : _baseSpeed*difficultyMultiplier;
        }

        public float SideSpeed(float difficultyMultiplier)
        {
            return IsBoosting ? _boostMultiplier * _lateralSpeed*difficultyMultiplier : _lateralSpeed*difficultyMultiplier;
        }

        public float DownForce(float difficultyMultiplier)
        {
            if (!IsFalling)
                return 0;
            
            return IsBoosting ? _boostMultiplier * _downSpeed * difficultyMultiplier : _downSpeed*difficultyMultiplier;
        }

        public bool IsBoosting = false;
        public bool IsFalling = false;
    }
}