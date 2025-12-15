using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _repeatRateTransparency;

    private readonly int _minRange = 2;
    private readonly int _maxRange = 5;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Color _color;

    private float _currentTimer;
    private float _repeatRateTimer;

    public event Action<Bomb> Activated;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _color = _meshRenderer.material.color;
    }

    private void OnEnable()
    {
        Activated?.Invoke(this);
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        int time = GetRandomTime();
        StartCoroutine(ChangeTransparency(time));
    }

    public void ResetSettings()
    {        
        transform.rotation = Quaternion.identity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        _meshRenderer.material.color = _color;
    }

    //private void Explose()
    //{
    //    Collider[] colliders = Physics.OverlapSphereNonAlloc(transform.position, _radiusExplosion);
    //}

    private IEnumerator ChangeTransparency(float time)
    {
        float a = 1;

        while (_color.a != 0)
        {
            a = Mathf.MoveTowards(1, 0, time * Time.deltaTime);
            //_meshRenderer.material.color = new Color(_color.r, _color.g, _color.b, a);
            Debug.Log($"Change: {_color.a > 0}, color: {a}");
            yield return null;
        }

        yield break;
    }

    private int GetRandomTime() =>
        Random.Range(_minRange, _maxRange + 1);
}
