
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pool
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        private Dictionary<string, Pool> poolDic;
        private Dictionary<string, GameObject> objCache;

        private static PoolManager instance;
        public static PoolManager Instance
        {
            get { return instance; }
        }

        void Awake()
        {
            instance = this;
            poolDic = new Dictionary<string, Pool>();
            objCache = new Dictionary<string, GameObject>();
        }

        public void CreatePool(string poolName, GameObject prefab, int count = 0)
        {
            Pool pool = Pool.Create(poolName, prefab, count);
            poolDic.Add(poolName, pool);
        }


        public Pool this[string poolName]
        {
            get { return poolDic.ContainsKey(poolName) ? poolDic[poolName] : null; }
        }

        public void Dispose()
        {
            foreach (Pool pool in poolDic.Values)
            {
                pool.Dispose();
            }
            poolDic.Clear();

            IEnumerable<string> keys = objCache.Keys;
            foreach (string key in keys)
            {
                Destroy(objCache[key]);
            }
            objCache.Clear();
        }


        #region Cache

        internal bool Contain(string type)
        {
            return objCache.ContainsKey(type);
        }

        internal GameObject CreateCacheObject(string type)
        {
            return GameObject.Instantiate(objCache[type]) as GameObject;
        }

        public void CacheGameObject(string key, GameObject g)
        {
            Debug.Log("[PoolManager]CacheGameObject");
            g.transform.SetParent(transform);
            g.SetActive(false);
            if (!objCache.ContainsKey(key))
            {
                objCache.Add(key, g);
            }
            else
            {
                GameObject go = objCache[key];
                objCache[key] = g;
                Destroy(go);
            }
        }

        #endregion
    }
}
