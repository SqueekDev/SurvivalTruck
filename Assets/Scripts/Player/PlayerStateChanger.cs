using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Player))]

public class PlayerStateChanger : MonoBehaviour
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private CameraPositionChanger _cameraPositionChanger;
    [SerializeField] private MoverToShootPlace _moverToShootPlace;
    [SerializeField] private BossShooter _bossShooter;
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Shooter _shooter;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private VariableJoystick _joystick;

    private Rigidbody _rigidbody;
    private Player _player;
    private bool _actionsDisabled;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _actionsDisabled = false;
        _levelChanger.BossLevelStarted += OnBossLevelStarted;
        _levelChanger.BossLevelEnded += OnBossLevelEnded;
        _cameraPositionChanger.Descended += OnCameraDescended;
        _cameraPositionChanger.Climbed += OnCameraClimbed;
    }

    private void Update()
    {
        if (_actionsDisabled == false && _player.IsDead)
        {
            DisableActions();
        }
    }

    private void OnDisable()
    {
        _levelChanger.BossLevelStarted -= OnBossLevelStarted;
        _levelChanger.BossLevelEnded -= OnBossLevelEnded;
        _cameraPositionChanger.Descended -= OnCameraDescended;
        _cameraPositionChanger.Climbed -= OnCameraClimbed;
    }

    private void DisableActions()
    {
        _joystick.gameObject.SetActive(false);
        _shooter.enabled = false;
        _playerMover.enabled = false;
        _playerInput.enabled = false;
        _cameraMover.enabled = false;
        _bossShooter.enabled = false;
        _moverToShootPlace.enabled = false;
        _actionsDisabled = true;
    }

    private void OnBossLevelStarted()
    {
        _shooter.enabled = false;
        _playerInput.enabled = false;
        _playerMover.enabled = false;
        _joystick.gameObject.SetActive(false);
        _rigidbody.isKinematic = true;
        _moverToShootPlace.enabled = true;
    }

    private void OnCameraDescended()
    {
        _moverToShootPlace.enabled = false;
        _joystick.gameObject.SetActive(true);
        _bossShooter.enabled = true;
        _cameraMover.enabled = true;
    }

    private void OnBossLevelEnded()
    {
        _bossShooter.enabled = false;
        _cameraMover.enabled = false;
        _joystick.gameObject.SetActive(false);
        _rigidbody.isKinematic = false;
    }

    private void OnCameraClimbed()
    {
        _joystick.gameObject.SetActive(true);
        _shooter.enabled = true;
        _playerInput.enabled = true;
        _playerMover.enabled = true;
    }
}
