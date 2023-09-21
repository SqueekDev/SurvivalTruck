using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private int _spawnPlatformCorrection;
    [SerializeField] private int _platformLimit;
    [SerializeField] private Car _car;
    [SerializeField] private Platform _platformTemplate;
    [SerializeField] private Platform _startPlatform;

    private List<Platform> _spawnedPlatforms = new List<Platform>();

    private void Start()
    {
        _spawnedPlatforms.Add(_startPlatform);
    }

    private void Update()
    {
        if (_car.transform.position.z > (_spawnedPlatforms[_spawnedPlatforms.Count - 1].transform.position.z - _spawnPlatformCorrection))
        {
            SpawnPlatform();
        }

        if (_spawnedPlatforms.Count >= _platformLimit)
            RemovePlatform();
    }

    private void SpawnPlatform()
    {
        Platform newPlatform = Instantiate(_platformTemplate, transform);
        newPlatform.transform.position = _spawnedPlatforms[_spawnedPlatforms.Count - 1].EndPoint.transform.position - newPlatform.StartPoint.localPosition;
        _spawnedPlatforms.Add(newPlatform);
    }

    private void RemovePlatform()
    {
        Destroy(_spawnedPlatforms[0].gameObject);
        _spawnedPlatforms.RemoveAt(0);
    }
}
