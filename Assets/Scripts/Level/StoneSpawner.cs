using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnSpread;
    [SerializeField] private int _spawnPositionOffset;
    [SerializeField] private int _rotationModifier;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private List<Stone> _templates;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        Vector3 spawnPosition = _startPoint.position;

        while (spawnPosition.z < _endPoint.position.z)
        {
            float spread = Random.Range(-_spawnSpread, _spawnSpread);
            Stone template = _templates[Random.Range(0, _templates.Count)];
            spawnPosition.x += spread;
            Stone stone = Instantiate(template, spawnPosition, Quaternion.identity, transform);
            spawnPosition.x -= spread;
            int startRotation = 90;
            float rotationY = startRotation * Random.Range(0, _rotationModifier);
            stone.transform.rotation = Quaternion.Euler(0, rotationY, 0);
            spawnPosition.z += _spawnPositionOffset;
        }
    }
}
