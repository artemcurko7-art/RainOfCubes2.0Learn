using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : Prefab
{
    public event Action<Cube> Collided;

    public bool IsCollision { get; private set; }

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
                MeshRenderer.material.color = Random.ColorHSV();
                StartCoroutine(StartWait());
            }
        }
    }

    public override void ResetSettings()
    {
        base.ResetSettings();
        IsCollision = false;
    }
}