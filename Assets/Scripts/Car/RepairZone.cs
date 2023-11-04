using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepairZone : MonoBehaviour
{
    [SerializeField] private float _timeToRepair;

    private Coroutine _repairCorutine;
    private float _currentTime;

    public event UnityAction Repaired;

    private IEnumerator Repair()
    {
        _currentTime = 0;

        while (_currentTime < _timeToRepair)
        {
            _currentTime += Time.deltaTime;
            yield return null;
        }

        Repaired?.Invoke();
    }

    private void StopRepair()
    {
        if (_repairCorutine != null)
            StopCoroutine(_repairCorutine);
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
