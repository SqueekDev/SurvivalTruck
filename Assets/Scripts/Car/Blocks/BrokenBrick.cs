using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBrick : MonoBehaviour
{
    [SerializeField] private List<WoodPiece> _woodPieces;
    [SerializeField] private Transform _explosionPoint;

    private int _explosionForce = 100;
    private float _explosionRadius = 5;
    private float _timeToDecreace = 0.5f;
    private float _decreacingSpeed = 1.2f;
    private float _decreacingTime = 1f;
    private Coroutine _brokeCorutine;

    private void OnEnable()
    {
        if (_brokeCorutine != null)
            StopCoroutine(_brokeCorutine);

        _brokeCorutine = StartCoroutine(BrokeBrick());
    }

    private IEnumerator BrokeBrick()
    {
        foreach (var woodPiece in _woodPieces)
        {
            woodPiece.transform.localPosition = woodPiece.StartPosition;
            woodPiece.transform.localRotation = woodPiece.StartRotation;
            woodPiece.transform.localScale = woodPiece.StartScale;
            woodPiece.Rigidbody.AddExplosionForce(_explosionForce, _explosionPoint.position, _explosionRadius);
        }

        yield return new WaitForSeconds(_timeToDecreace);
        Vector3 targetScale = new Vector3(0, 0, 0);
        float timer = 0;

        while (timer < _decreacingTime)
        {
            foreach (var woodPiece in _woodPieces)
                woodPiece.transform.localScale = Vector3.Lerp(woodPiece.transform.localScale, targetScale, timer * _decreacingSpeed * Time.deltaTime);

            timer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
