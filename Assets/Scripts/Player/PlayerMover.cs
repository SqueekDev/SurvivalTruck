using UnityEngine;

public class PlayerMover : Mover
{
    [SerializeField] private Vector2 _xLimits;
    [SerializeField] private Vector2 _zLimits;
    [SerializeField] private Body _body;

    public void Move(Vector3 direction)
    {
        Vector3 target = new Vector3(transform.position.x + direction.x * Time.deltaTime * Speed,
                transform.position.y, transform.position.z + direction.z * Time.deltaTime * Speed);
        if (target.x > _xLimits.y && target.x < _xLimits.x&& target.z > _body.transform.position.z+_zLimits.y 
            && target.z < _body.transform.position.z + _zLimits.x)
        {
            transform.position=target ;
        }

    }
}
