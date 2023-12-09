using System.Collections.Generic;
using UnityEngine;

public class EnviromentElements : MonoBehaviour
{
    [SerializeField] private List<GameObject> _templates;

    private int _maxRotation = 360;
    private float _minScale = 0.7f;
    private float _maxScale = 1f;

    private void Start()
    {
        int currentTemplateNumber = Random.Range(0, _templates.Count);
        int yRotarion = Random.Range(0, _maxRotation);
        Quaternion rotation = Quaternion.Euler(0, yRotarion, 0);
        Instantiate(_templates[currentTemplateNumber], transform.position, rotation, transform);
        float scaleModifier = Random.Range(_minScale, _maxScale);
        transform.localScale *= scaleModifier;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnviromentElements element))
            Destroy(gameObject);
    }
}
