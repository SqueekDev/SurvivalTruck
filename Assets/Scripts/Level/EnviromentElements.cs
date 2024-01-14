using System.Collections.Generic;
using Base;
using UnityEngine;

namespace Level
{
    public class EnviromentElements : MonoBehaviour
    {
        private const int MaxRotation = 360;
        private const float MinScale = 0.7f;
        private const float MaxScale = 1f;

        [SerializeField] private List<GameObject> _templates;

        private void Start()
        {
            int currentTemplateNumber = Random.Range(GlobalValues.Zero, _templates.Count);
            int yRotarion = Random.Range(GlobalValues.Zero, MaxRotation);
            Quaternion rotation = Quaternion.Euler(GlobalValues.Zero, yRotarion, GlobalValues.Zero);
            Instantiate(_templates[currentTemplateNumber], transform.position, rotation, transform);
            float scaleModifier = Random.Range(MinScale, MaxScale);
            transform.localScale *= scaleModifier;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnviromentElements element))
            {
                Destroy(gameObject);
            }
        }
    }
}