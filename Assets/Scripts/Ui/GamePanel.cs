using System;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public event Action Opened;
    public event Action Closed;

    protected virtual void OnEnable()
    {
        Opened?.Invoke();
    }

    protected virtual void OnDisable()
    {
        Closed?.Invoke();
    }
}
