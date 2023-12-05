using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairZone : MonoBehaviour
{
    [SerializeField] private float _timeToRepair;
    [SerializeField] private ParticleSystem _repairParticles;
    [SerializeField] private Instruments _flyingInstruments;
    [SerializeField] private Instruments _repairInstruments;

    private Coroutine _repairCorutine;
    private float _currentTime;

    public event UnityAction Repaired;

    private void OnEnable()
    {
        _flyingInstruments.gameObject.SetActive(true);
    }

    private IEnumerator Repair()
    {
        _flyingInstruments.gameObject.SetActive(false);
        _repairInstruments.gameObject.SetActive(true);
        _currentTime = 0;
        _repairParticles.Play();

        while (_currentTime < _timeToRepair)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }

        _repairParticles.Play();
        Repaired?.Invoke();
        _repairInstruments.gameObject.SetActive(false);
    }

    private void StopRepair()
    {
        if (_repairCorutine != null)
            StopCoroutine(_repairCorutine);

        _repairInstruments.gameObject.SetActive(false);
        _flyingInstruments.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StopRepair();
            _repairCorutine = StartCoroutine(Repair());
        }            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            StopRepair();
    }
}
