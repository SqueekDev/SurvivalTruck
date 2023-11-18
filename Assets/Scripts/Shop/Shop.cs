using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Animator _coverOpeningAnimator;

    public void Open()
    {
        _coverOpeningAnimator.SetTrigger("Open");
    }
    public void Close()
    {
        _coverOpeningAnimator.SetTrigger("Close");
    }
}
