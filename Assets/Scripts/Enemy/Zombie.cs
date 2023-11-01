using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class Zombie : MonoBehaviour
{
    [SerializeField] private ZombieMover _zombieMover;
    [SerializeField] private ZombieAttacker _zombieAttacker;
    [SerializeField] private Player _player;

    private Transform _target;
    private Obstacle _obstacle;
    private Animator _animator;
    private Vector3 _startPosition;

    private void OnEnable()
    {
        _zombieAttacker.OnObstacleDestroyed += OnOstacleDestroyed;
    }
    private void OnDisable()
    {
        _zombieAttacker.OnObstacleDestroyed -= OnOstacleDestroyed;

    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _target=_player.transform;
        _startPosition = transform.position;
    }

    private void Update()
    {
            Vector3 attackDestination = new Vector3(_target.transform.position.x,_target.position.y,transform.position.z);
            _animator.SetFloat("attackDistance", Vector3.Distance(transform.position, attackDestination));

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RageArea rageArea))
        {
            SetObstacle(rageArea.Obstacle);
            _animator.SetTrigger("MoveTo");
        }
        if (other.TryGetComponent(out JumpTrigger jumpTrigger))
        {
            _animator.SetTrigger("Jump");
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Kangaroo kangaroo))
        {
            _animator.SetTrigger("Kangaroo");
        }
    }
    private void OnOstacleDestroyed()
    {
        _target = _player.transform;
    }
    public void SetObstacle(Obstacle obstacle)
    {
        _obstacle = obstacle;
    }

    public Transform GetTarget()
    {
        if (_obstacle!=null&&_obstacle.IsDestroyed==false)
        {
            _target = _obstacle.transform;
            return _target;
        }
        else
        {
            _target = _player.transform;
            return _target;
        }
    }
}
