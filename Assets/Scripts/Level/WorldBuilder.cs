using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    private const int ListIndexCorrection = 1;

    [SerializeField] private LevelChanger _levelChanger;
    [SerializeField] private int _spawnPlatformCorrection;
    [SerializeField] private int _platformLimit;
    [SerializeField] private Car _car;
    [SerializeField] private List<Platform> _platformTemplates;
    [SerializeField] private Fog _fogPrefab;

    private bool _needToAddFog = false;
    private int _changePlatformLevelNumberDivider;
    private int _previousPlatformIndex = 0;
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
        if (_car.transform.position.z > (_spawnedPlatforms[_spawnedPlatforms.Count - ListIndexCorrection].transform.position.z - _spawnPlatformCorrection))
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
        newPlatform.transform.position = _spawnedPlatforms[_spawnedPlatforms.Count - ListIndexCorrection].EndPoint.transform.position + (newPlatform.transform.position - newPlatform.StartPoint.position);
        Vector3 endPosition = _spawnedPlatforms[_spawnedPlatforms.Count - ListIndexCorrection].EndPoint.transform.position;

        if (_needToAddFog)
        {
            if (_endFog == null)
                _endFog = Instantiate(_fogPrefab, endPosition, Quaternion.identity);
            else
                _endFog.transform.position = endPosition;

            _needToAddFog = false;
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
        int platformIndex = levelNumber / _changePlatformLevelNumberDivider;

        while (platformIndex > _platformTemplates.Count - ListIndexCorrection)
            platformIndex -= _platformTemplates.Count;

        if (_previousPlatformIndex != platformIndex)
            _needToAddFog = true;

        _previousPlatformIndex = platformIndex;
        _currentPlatformTemplate = _platformTemplates[platformIndex];
    }
}
