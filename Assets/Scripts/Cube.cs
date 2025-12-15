using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private Color _color;

    public event Action<Cube> Collided;

    public bool IsCollision { get; private set; }

    public void Initialize(Vector3 position)
    { 
        transform.position = position;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _color = _meshRenderer.material.color;
    }

    private IEnumerator StartWait()
    {
        yield return new WaitForSeconds(GetRandomTimeLife());

        Collided?.Invoke(this);
        ResetSettings();
    }

    private int GetRandomTimeLife()
    {
        int minRange = 2;
        int maxRange = 5;

        int timeLife = Random.Range(minRange, maxRange + 1);

        return timeLife;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Obstacle>(out Obstacle obstacle))
        {
            if (IsCollision == false)
            {
                IsCollision = true;
                _meshRenderer.material.color = Random.ColorHSV();
                StartCoroutine(StartWait());
            }
        }
    }

    public void ResetSettings()
    {
        _meshRenderer.material.color = _color;
        transform.rotation = Quaternion.identity;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.linearVelocity = Vector3.zero;
        IsCollision = false;
    }
}