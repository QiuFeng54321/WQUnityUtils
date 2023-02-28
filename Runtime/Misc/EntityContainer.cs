using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Misc
{
    /// <summary>
    ///     与GameObject绑定的List
    /// </summary>
    /// <typeparam name="TEntity">存储的Entity类型，继承自MonoBehaviour</typeparam>
    /// <typeparam name="TContainer">内部存储Entity的数据结构</typeparam>
    public class EntityContainer<TEntity, TContainer> : MonoBehaviour, ICollection<TEntity>
        where TEntity : MonoBehaviour where TContainer : ICollection<TEntity>
    {
        public GameObject container;
        public GameObject entityPrefab;
        protected readonly TContainer Entities = Activator.CreateInstance<TContainer>();

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(TEntity item)
        {
            if (item == null) return;
            item.transform.parent = container.transform;
            Entities.Add(item);
            EntityAdded?.Invoke(item);
        }

        public void Clear()
        {
            container.transform.ClearChildren();
            foreach (var entity in Entities) EntityCleared?.Invoke(entity);
            Entities.Clear();
        }

        public bool Contains(TEntity item)
        {
            return Entities.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            Entities.CopyTo(array, arrayIndex);
        }

        public bool Remove(TEntity item)
        {
            if (item == null) return false;
            item.transform.parent = null;
            EntityRemoved?.Invoke(item);
            return Entities.Remove(item);
        }

        public int Count => Entities.Count;
        public bool IsReadOnly => false;

        public event Action<TEntity> EntityAdded;
        public event Action<TEntity> EntityRemoved;
        public event Action<TEntity> EntityCleared;

        public TEntity Add()
        {
            var entity = Instantiate(entityPrefab, container.transform);
            var item = entity.GetComponent<TEntity>();
            Add(item);
            return item;
        }
    }
}