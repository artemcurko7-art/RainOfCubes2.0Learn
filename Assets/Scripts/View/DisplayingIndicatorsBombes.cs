using UnityEngine;

public class DisplayingIndicatorsBombes : DisplayingIndicators
{
    [SerializeField] private SpawnerBombes _spawnerBombes;

    private void OnEnable() =>
        _spawnerBombes.Viewed += View;

    private void OnDisable() =>
        _spawnerBombes.Viewed -= View;
}
