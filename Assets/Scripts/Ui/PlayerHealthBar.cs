using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Image _filledHealthBar;
    [SerializeField] private PlayerHealth _player;
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
        Vector3 forward = transform.position - _camera.transform.position;
        forward.Normalize();
        Vector3 up = Vector3.Cross(forward, _camera.transform.right);
        transform.rotation = Quaternion.LookRotation(forward, up);
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
