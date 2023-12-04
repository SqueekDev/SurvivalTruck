using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Zombie : MonoBehaviour
{
    [SerializeField] private ZombieMover _zombieMover;
    [SerializeField] private ZombieAttacker _zombieAttacker;
    [SerializeField] private ZombieHealth _zombieHealth;
    [SerializeField] private Player _player;
    [SerializeField] private float _maximumDistanceToTarget;
    [SerializeField] private AudioSource _kangarooCollision;

    private Transform _target;
    private Obstacle _obstacle;
    private Animator _animator;

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
        _target = _player.transform;
    }

    private void Update()
    {
        _target = GetTarget();
        if (_target.TryGetComponent(out Obstacle obstacle))
        {
            Vector3 newPosition = new Vector3(_target.transform.position.x, _target.transform.position.y,transform.position.z);
            _animator.SetFloat("attackDistance", Vector3.Distance(transform.position, newPosition));

        }
        else
        {
            _animator.SetFloat("attackDistance", Vector3.Distance(transform.position, _target.position));

        }
        if (transform.position.y < 0)
        {
            _zombieHealth.Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out RageArea rageArea))
        {
            SetObstacle(rageArea.Obstacle);
            _animator.SetTrigger("MoveTo");
            _zombieHealth.SetAngry();
        }
        if (other.TryGetComponent(out JumpTrigger jumpTrigger))
        {
            _zombieMover.SetJumpPoint(jumpTrigger.JumpPoint);
            _animator.SetTrigger("Jump");
            Debug.Log("j");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Kangaroo kangaroo))
        {
            _animator.SetTrigger("Kangaroo");
            _kangarooCollision.Play();
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
        if (_obstacle != null && _obstacle.IsDestroyed == false)
        {
            _target = _obstacle.transform;
        }
        else
        {
            _target = _player.transform;
        }
        return _target;
    }
}
