using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bomb : Prefab
{
    [SerializeField] private LayerMask _prefabMask;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private readonly int _minRange = 2;
    private readonly int _maxRange = 5;

    private Collider[] _colliders = new Collider[32];

    public event Action<Bomb> Activated;

    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);
        StartCoroutine(ChangeTransparency(GetRandomDuration()));
    }

    private IEnumerator ChangeTransparency(float duration)
    {
        float colorA = 1;

        while (colorA > 0)
        {
            colorA = Mathf.MoveTowards(colorA, 0, duration * Time.deltaTime);
            MeshRenderer.material.color = new Color(Color.r, Color.g, Color.b, colorA);
            yield return null;
        }

        Explose();

        yield break;
    }

    private void Explose()
    {
        int result = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _colliders, _prefabMask);

        for (int i = 0; i < result; i++)
        {
            if (_colliders[i].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
        }

        Activated?.Invoke(this);
    }

    private int GetRandomDuration() =>
        Random.Range(_minRange, _maxRange + 1);
}
