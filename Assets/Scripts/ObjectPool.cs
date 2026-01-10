using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly int _maxCount;
    private readonly int _expandCount;

    private readonly Transform _parent;
    private readonly T _prefab;

    private readonly Queue<T> _pool = new();
    private int _totalCreated;

    public ObjectPool(
        T prefab,
        Transform parent,
        int initialSize = 10,
        int maxCount = 100,
        int expandCount = 20)
    {
        _prefab = prefab;
        _parent = parent;
        _maxCount = maxCount;
        _expandCount = expandCount;

        CreatePool(initialSize);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_totalCreated >= _maxCount)
                return;

            var obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);

            _pool.Enqueue(obj);
            _totalCreated++;
        }
    }

    /// <summary>
    /// Havuz boşsa otomatik genişler
    /// </summary>
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

    /// <summary>
    /// Objeyi havuza geri bırakır
    /// </summary>
    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }

    /// <summary>
    /// Pool'u kontrollü şekilde büyütür
    /// </summary>
    public void Expand()
    {
        if (_totalCreated >= _maxCount)
            return;

        int available = _maxCount - _totalCreated;
        int countToCreate = Mathf.Min(_expandCount, available);

        CreatePool(countToCreate);
    }
}
