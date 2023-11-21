using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _filledHealthBar;
    [SerializeField] private Player _player;
    [SerializeField] private Gradient _gradient;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;        
    }

    private void OnHealthChanged(float healthPercentage)
    {
        _filledHealthBar.fillAmount = healthPercentage;
        _filledHealthBar.color = _gradient.Evaluate(healthPercentage);

    }
}
