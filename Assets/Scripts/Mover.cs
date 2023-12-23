using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _stopDistance=0.5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] protected float _startSpeed;
    [SerializeField] protected float Speed;

    protected Rigidbody Rigidbody;
    protected float _currentSpeed;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = Speed;
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position,_stopDistance,_layerMask)) 
        {
            Speed = 1f;
        }
        else
        {
            Speed = _currentSpeed;
        }
    }
    public void Rotate(Vector3 destination)
    {
        Vector3 direction = new Vector3(destination.x, transform.position.y, destination.z) - transform.position;
        Quaternion newDirection = transform.rotation;

        if (direction != Vector3.zero)
        {
            newDirection = Quaternion.LookRotation(direction);

        }

        transform.rotation = Quaternion.Lerp(transform.rotation, newDirection, _rotationSpeed * Time.deltaTime);
    }

    public void SetNoSpeed()
    {
        Speed = GlobalValues.Zero;
    }

    public void SetStartSpeed()
    {
        Speed = _startSpeed;
    }
}
