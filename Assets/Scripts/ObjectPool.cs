using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly int _maxCount;
    private readonly int _expandCount;

    private readonly Transform _parent;
    private readonly T _prefab;

    private readonly Queue<T> _pool = new();
    private int _totalCreated;
    private IObjectResolver _resolver;

    public ObjectPool(
        IObjectResolver resolver,
        T prefab,
        [CanBeNull] Transform parent,
        int initialSize = 10,
        int maxCount = 100,
        int expandCount = 20)
    {
        _resolver = resolver;
        _prefab = prefab;
        _parent = parent;
        _maxCount = maxCount;
        _expandCount = expandCount;

        if (_parent == null)
        {
            var gameObject = new GameObject(prefab.name);
            _parent = gameObject.transform;
        }

        CreatePool(initialSize);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_totalCreated >= _maxCount)
                return;

            var obj = _resolver.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);

            _pool.Enqueue(obj);
            _totalCreated++;
        }
    }

    public T GetObject()
    {
        if (_pool.Count == 0)
        {
            Expand();
        }

        if (_pool.Count == 0)
        {
            Debug.LogWarning("POOL MAX SIZE REACHED");
            return null;
        }

        var obj = _pool.Dequeue();
        obj.gameObject.SetActive(true);
        return obj;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    public void Expand()
    {
        if (_totalCreated >= _maxCount)
            return;

        int available = _maxCount - _totalCreated;
        int countToCreate = Mathf.Min(_expandCount, available);

        CreatePool(countToCreate);
    }
}