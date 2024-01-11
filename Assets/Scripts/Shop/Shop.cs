using System.Collections;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const float OpenDelay = 1f;
    private const string OpenTrigger = "Open";
    private const string CloseTrigger = "Close";

    [SerializeField] private Animator _coverOpeningAnimator;
    [SerializeField] private AudioSource _coverOpeningSound;
    [SerializeField] private AudioSource _coverClosingSound;
    [SerializeField] private GamePanel _upgradePanel;
    [SerializeField] private ShopView _shopView;
    [SerializeField] private LevelChanger _levelChanger;

    private WaitForSeconds _delayBetweenOpening = new WaitForSeconds(OpenDelay);

    private void OnEnable()
    {
        _shopView.Closed += Close;
    }

    private void OnDisable()
    {
        _shopView.Closed -= Close;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Shooter player) && _levelChanger.IsWave == false)
        {
            StartCoroutine(CoverOpening());
        }
    }

    private void Open()
    {
        _coverOpeningAnimator.SetTrigger(OpenTrigger);
        _coverOpeningSound.Play();
    }
    private void Close()
    {
        _coverOpeningAnimator.SetTrigger(CloseTrigger);
        _coverClosingSound.Play();
    }

    private IEnumerator CoverOpening()
    {
        Open();
        yield return _delayBetweenOpening;
        _upgradePanel.gameObject.SetActive(true);
    }
}
