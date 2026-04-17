using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PoolMono<T> where T : MonoBehaviour
{
    private List<T> pool;
    
    public IEnumerable<T> Pool => pool;
    
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    public PoolMono(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;
        this.CreatePool(count);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        this.CreatePool(count);
    }

    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;

        if (this.autoExpand)
            return this.CreateObject(true);

        throw new Exception($"There is no free elements in pool of type {typeof(T)}");
    }

    public void ResetElements()
    {
        foreach (var item in this.pool)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void ResetElement(T element)
    {
        foreach (var item in this.pool)
        {
            if (item == element)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++)
            this.CreateObject();
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                return true;
            }
        }

        element = null;
        return false;
    }
}