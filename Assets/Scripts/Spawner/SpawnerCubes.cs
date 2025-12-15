using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCubes : Spawner<Cube>
{
    [SerializeField] private float _repeatRate;

    public event Action<Cube> Disabled;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        cube.Initialize(GetRandomPosition());
        cube.Collided += OnRelease;
    }

    public override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);
        cube.ResetSettings();
        Disabled?.Invoke(cube);
    }   

    public override void OnRelease(Cube cube)
    {
        base.OnRelease(cube);
        cube.Collided -= OnRelease;
    }

    private IEnumerator Spawn()
    {
        float elapsedTime = 0;

        while (elapsedTime <= _repeatRate)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= _repeatRate)
            {
                GetPrefab();
                elapsedTime = 0;
            }

            yield return null;
        }
    }

    private Vector3 GetRandomPosition()
    {
        int indexPosition = Random.Range(0, transform.childCount);
        Vector3 position = transform.GetChild(indexPosition).position;

        return position;
    }
}
