using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleRotator : MonoBehaviour
{
    private Vector3 _rotationAxis;
    private float _speed;
    private Vector3 _scale;

    private void Start()
    {
        _rotationAxis = Random.onUnitSphere;
        _speed = Random.Range(10f, 50f);
        _scale = transform.localScale;
        transform.DOScale(_scale,.5f).From(Vector3.zero);
    }

    private void Update()
    {
        transform.Rotate(_rotationAxis * _speed * Time.deltaTime);
    }
}
