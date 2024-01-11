using System.Collections;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private const float ChangeColorDelayTime = 0.2f;

    [SerializeField] private Color _damageColor;
    [SerializeField] private Color _deathColor;
    [SerializeField] private Renderer _renderer;

    private Coroutine _changingColor;
    private Color _startColor;
    private WaitForSeconds _changeColorDelay = new WaitForSeconds(ChangeColorDelayTime);

    private void Start()
    {
        _startColor = _renderer.material.color;
    }

    public void ChangeToDamageColor()
    {
        if (_changingColor == null)
        {
            _changingColor = StartCoroutine(ChangingColorDamage());
        }
    }

    private IEnumerator ChangingColorDamage()
    {
        _renderer.sharedMaterial.color = _damageColor;
        yield return _changeColorDelay;
        _renderer.sharedMaterial.color = _startColor;
        _changingColor = null;
    }

    public void ChangeColorDeath()
    {
        if (_changingColor != null)
        {
            StopCoroutine(_changingColor);
        }

        _renderer.sharedMaterial.color = _deathColor;
    }

    public void ChangeColorNormal()
    {
        _renderer.sharedMaterial.color = _startColor;
    }
}
