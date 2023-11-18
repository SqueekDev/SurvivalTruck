using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentElements : MonoBehaviour
{
    [SerializeField] private List<GameObject> _templates;

    private int _maxRotation = 360;

    private void Start()
    {
        int currentTemplateNumber = Random.Range(0, _templates.Count);
        int yRotarion = Random.Range(0, _maxRotation);
        Quaternion rotation = Quaternion.Euler(0, yRotarion, 0);
        Instantiate(_templates[currentTemplateNumber], transform.position, rotation, transform);
    }
}
