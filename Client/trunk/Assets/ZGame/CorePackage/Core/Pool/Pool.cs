
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pool
{
    /// <summary>
    /// 功能概述：
    /// </summary>
    public class Pool : MonoBehaviour
    {
        private GameObject prefab;
        private int preCount;
        private Transform trans;

        private Queue<GameObject> freeList;
        private Queue<GameObject> useList;

        void Awake()
        {
            trans = transform;
            freeList = new Queue<GameObject>();
            useList = new Queue<GameObject>();
        }

        public static Pool Create(string name, GameObject prefab, int preCount = 0)
        {
            GameObject go = new GameObject(name);
            Pool pool = go.AddComponent<Pool>();
            pool.prefab = prefab;
            if (preCount > 0)
            {
                pool.preCount = preCount;
                pool.Preload();
            }

            return pool;
        }

        private void Preload()
        {
            if (prefab == null)
            {
                D.error("pool prefab is null");
                return;
            }

            for (int i = 0; i < preCount; i++)
            {
                GameObject go = GameObject.Instantiate(prefab) as GameObject;
                go.SetActive(false);
                freeList.Enqueue(go);
            }
        }

        public GameObject Spawn()
        {
            return GetFree();
        }

        private GameObject GetFree()
        {
            if (freeList.Count > 0)
            {
                GameObject go = freeList.Dequeue();
                useList.Enqueue(go);
                go.SetActive(true);
                return go;
            }
            else
            {
                if (prefab == null)
                {
                    D.error("pool prefab is null");
                    return null;
                }
                GameObject go = GameObject.Instantiate(prefab) as GameObject;
                useList.Enqueue(go);
                return go;
            }
        }

        public void DeSpawn(GameObject go)
        {
            go.transform.SetParent(trans);
            go.SetActive(false);
            freeList.Enqueue(useList.Dequeue());
        }

        internal void Dispose()
        {
            freeList.Clear();
            useList.Clear();
        }
    }
}
