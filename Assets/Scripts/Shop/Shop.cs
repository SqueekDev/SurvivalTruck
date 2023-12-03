using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Animator _coverOpeningAnimator;
    [SerializeField] private AudioSource _coverOpeningSound;
    [SerializeField] private AudioSource _coverClosingSound;

    public void Open()
    {
        _coverOpeningAnimator.SetTrigger("Open");
        _coverOpeningSound.Play();
    }
    public void Close()
    {
        _coverOpeningAnimator.SetTrigger("Close");
        _coverClosingSound.Play();
    }
}
