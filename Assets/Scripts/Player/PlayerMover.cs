using UnityEngine;

public class PlayerMover : Mover
{
    public void Move(Vector3 direction)
    {

        Rigidbody.AddForce(direction*Speed, ForceMode.Impulse);
    }
}
