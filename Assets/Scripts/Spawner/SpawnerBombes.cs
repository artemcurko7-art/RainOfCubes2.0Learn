using System;
using UnityEngine;

public class SpawnerBombes : Spawner<Bomb>
{
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void OnEnable()
    {
        _spawnerCubes.Disabled += DisableCube;
    }

    private void OnDisable()
    {
        _spawnerCubes.Disabled -= DisableCube;
    }

    public override void ActionOnGet(Bomb bomb)
    {
        base.ActionOnGet(bomb);
        bomb.Activated += OnRelease;
    }

    public override void ActionOnRelease(Bomb bomb)
    {
        base.ActionOnRelease(bomb);
        bomb.ResetSettings();
    }

    public override void OnRelease(Bomb bomb)
    {
        base.OnRelease(bomb);
        bomb.Activated -= OnRelease;
    }

    private void DisableCube(Cube cube) =>
        GetPrefab().Initialize(cube.transform.position);
}
