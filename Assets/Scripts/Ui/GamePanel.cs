using UnityEngine;
using UnityEngine.Events;

public class GamePanel : MonoBehaviour
{
    public event UnityAction Opened;
    public event UnityAction Closed;

    protected virtual void OnEnable()
    {
        Opened?.Invoke();
    }

    protected virtual void OnDisable()
    {
        Closed?.Invoke();
    }
}
