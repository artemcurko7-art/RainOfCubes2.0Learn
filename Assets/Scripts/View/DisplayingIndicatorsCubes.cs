using UnityEngine;

public class DisplayingIndicatorsCubes : DisplayingIndicators
{
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void OnEnable() =>
        _spawnerCubes.Viewed += View;

    private void OnDisable() =>
        _spawnerCubes.Viewed -= View;
}
