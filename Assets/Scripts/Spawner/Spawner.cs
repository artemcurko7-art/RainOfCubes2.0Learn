using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner <T>: MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (prefab) => ActionOnGet(prefab),
            actionOnRelease: (prefab) => ActionOnRelease(prefab));    
    }

    public virtual void ActionOnGet(T prefab) =>
        prefab.gameObject.SetActive(true);

    public virtual void ActionOnRelease(T prefab) =>
        prefab.gameObject.SetActive(false);

    public virtual void OnRelease(T prefab) =>
        _pool.Release(prefab);

    protected T GetPrefab() =>
        _pool.Get();
}
