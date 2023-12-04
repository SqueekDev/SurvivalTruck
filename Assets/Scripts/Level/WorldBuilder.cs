using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private int _spawnPlatformCorrection;
    [SerializeField] private int _platformLimit;
    [SerializeField] private Car _car;
    [SerializeField] private List<Platform> _platformTemplates;
    [SerializeField] private Fog _fogPrefab;

    private int _changePlatformLevelNumberDivider;
    private Platform _currentPlatformTemplate;
    private Fog _startFog;
    private Fog _endFog;
    private List<Platform> _spawnedPlatforms = new List<Platform>();

    private void Awake()
    {
        _changePlatformLevelNumberDivider = _levelChanger.BossLevelNumber;
    }

    private void OnEnable()
    {
        _levelChanger.Changed += OnLevelChanged;
    }

    private void Start()
    {
        OnLevelChanged(_levelChanger.CurrentLevelNumber);
        Platform startPlatform = Instantiate(_currentPlatformTemplate, transform);
        _spawnedPlatforms.Add(startPlatform);
    }

    private void Update()
    {
        if (_car.transform.position.z > (_spawnedPlatforms[_spawnedPlatforms.Count - 1].transform.position.z - _spawnPlatformCorrection))
            SpawnPlatform();

        if (_spawnedPlatforms.Count >= _platformLimit)
            RemovePlatform();
    }

    private void OnDisable()
    {
        _levelChanger.Changed -= OnLevelChanged;
    }

    private void SpawnPlatform()
    {
        Platform newPlatform = Instantiate(_currentPlatformTemplate, transform);
        newPlatform.transform.position = _spawnedPlatforms[_spawnedPlatforms.Count - 1].EndPoint.transform.position + (newPlatform.transform.position - newPlatform.StartPoint.position);
        Vector3 startPosition = _spawnedPlatforms[_spawnedPlatforms.Count - 1].StartPoint.transform.position;
        Vector3 endPosition = _spawnedPlatforms[_spawnedPlatforms.Count - 1].EndPoint.transform.position;
        if (_startFog==null)
        {
            _startFog = Instantiate(_fogPrefab, startPosition, Quaternion.identity);
        }
        else
        {
            _startFog.transform.position = startPosition;
        }
        if (_endFog==null)
        {
            _endFog = Instantiate(_fogPrefab, endPosition, Quaternion.identity);

        }
        else
        {
            _endFog.transform.position = endPosition;
        }
        _spawnedPlatforms.Add(newPlatform);
    }

    private void RemovePlatform()
    {
        Destroy(_spawnedPlatforms[0].gameObject);
        _spawnedPlatforms.RemoveAt(0);
    }

    private void OnLevelChanged(int levelNumber)
    {
        int listIndexCorrection = 1;
        int platformIndex = (levelNumber - listIndexCorrection) / _changePlatformLevelNumberDivider;

        while (platformIndex > _platformTemplates.Count - listIndexCorrection)
            platformIndex -= _platformTemplates.Count;

        _currentPlatformTemplate = _platformTemplates[platformIndex];
    }
}
