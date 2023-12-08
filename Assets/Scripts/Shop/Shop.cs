using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const float OpenDelay = 1f;

    [SerializeField] private Animator _coverOpeningAnimator;
    [SerializeField] private AudioSource _coverOpeningSound;
    [SerializeField] private AudioSource _coverClosingSound;
    [SerializeField] private UpgradesPanel _upgradesPanel;
    [SerializeField] private LevelChanger _levelChanger;

    private void OnEnable()
    {
        _upgradesPanel.Closed += Close;
    }

    private void OnDisable()
    {
        _upgradesPanel.Closed -= Close;        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Shooter player) && _levelChanger.IsWave == false)
            StartCoroutine(CoverOpening());
    }

    private void Open()
    {
        _coverOpeningAnimator.SetTrigger("Open");
        _coverOpeningSound.Play();
    }
    private void Close()
    {
        _coverOpeningAnimator.SetTrigger("Close");
        _coverClosingSound.Play();
    }

    private IEnumerator CoverOpening()
    {
        Open();
        yield return new WaitForSeconds(OpenDelay);
        _upgradesPanel.gameObject.SetActive(true);
    }
}
