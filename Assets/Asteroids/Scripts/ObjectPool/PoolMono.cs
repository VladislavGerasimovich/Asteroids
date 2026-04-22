using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Asteroids.Scripts.ObjectPool
{
    public class PoolMono<T> where T : MonoBehaviour
    {
        private List<T> _pool;

        public IEnumerable<T> Pool => _pool;
        public T Prefab { get; }
        public bool AutoExpand { get; set; }
        public Transform Container { get; }

        public PoolMono(T prefab, int count)
        {
            this.Prefab = prefab;
            this.Container = null;
            this.CreatePool(count);
        }

        public PoolMono(T prefab, int count, Transform container)
        {
            this.Prefab = prefab;
            this.Container = container;
            this.CreatePool(count);
        }

        public T GetFreeElement()
        {
            if (this.HasFreeElement(out var element))
                return element;

            if (this.AutoExpand)
                return this.CreateObject(true);

            throw new Exception($"There is no free elements in pool of type {typeof(T)}");
        }

        public void ResetElements()
        {
            foreach (var item in this._pool)
            {
                item.gameObject.SetActive(false);
            }
        }

        public void ResetElement(T element)
        {
            foreach (var item in this._pool)
            {
                if (item == element)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }

        private void CreatePool(int count)
        {
            this._pool = new List<T>();
            for (int i = 0; i < count; i++)
                this.CreateObject();
        }

        private T CreateObject(bool isActiveByDefault = false)
        {
            var createdObject = Object.Instantiate(this.Prefab, this.Container);
            createdObject.gameObject.SetActive(isActiveByDefault);
            this._pool.Add(createdObject);
            return createdObject;
        }

        private bool HasFreeElement(out T element)
        {
            foreach (var mono in _pool)
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
}