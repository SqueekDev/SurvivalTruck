using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] protected float _startSpeed;

    [SerializeField] protected float Speed;

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
