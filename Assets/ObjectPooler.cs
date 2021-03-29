using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parent;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public bool StaticParent;
    // Start is called before the first frame update
    void Awake()
    {
        Transform staticObj = null;
        StaticArea staticArea = null;
        try
        {
            staticObj = GameObject.FindGameObjectWithTag("Static").transform;
            staticArea = staticObj.GetComponent<StaticArea>();
        }
        catch
        {
            Debug.LogWarning("Static object does't exist");
        }
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = null;
                if (pool.parent == null)
                {
                    if (StaticParent)
                    {
                        obj = Instantiate(pool.prefab, staticObj);
                        staticArea.AddObj(obj);
                    }
                    else
                    {
                        obj = Instantiate(pool.prefab);
                    }
                }
                else
                {
                    obj = Instantiate(pool.prefab, pool.parent);
                    obj.transform.position = pool.parent.position;
                }
                objectPool.Enqueue(obj);
                obj.SetActive(false);
                //staticArea.AddObj(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
      
     
    }

    /// <summary>
    /// spawn object in the pool
    /// </summary>
    /// <param name="tag">
    /// pool tag
    /// </param>
    /// <param name="pos">
    /// spawn position
    /// </param>
    /// <param name="rotation">spawn Quaternion</param>
    /// <returns></returns>
    public GameObject SpawnFromPool(string tag,Vector3 pos,Quaternion rotation)
    {
        var obj = SpawnFromPool(tag, rotation);
        obj.transform.position = pos;
        return obj;
    }

    public GameObject SpawnFromPool(string tag, Quaternion rotation)
    {

        var obj = SpawnFromPool(tag);
        obj.transform.rotation = rotation;
        return obj;
    }
    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("pool \"" + tag + "\" does't exist");
            return null;
        }
        GameObject obj = poolDictionary[tag].Peek();
        if (checkObjectActive(obj))
        {
            //GameObject obj = null;
            try
            {
            Transform staticObj = GameObject.FindGameObjectWithTag("Static").transform;
                if (obj.transform.parent == staticObj)
                {
                    obj = Instantiate(obj, staticObj);
                    var staticArea = staticObj.GetComponent<StaticArea>();
                    staticArea.AddObj(obj);
                }
                else
                {
                    obj = Instantiate(obj, obj.transform.parent);
                    if (obj.transform.parent)
                    {
                        obj.transform.position = obj.transform.parent.position;
                    }
                }
         
            }
            catch
            {
                obj = Instantiate(obj, obj.transform.parent);
                if (obj.transform.parent)
                {
                    obj.transform.position = obj.transform.parent.position;
                }
            }
        }
        else
        {
            obj = poolDictionary[tag].Dequeue();
            obj.SetActive(true);

        }

        PooledObject[] poolObj = obj.GetComponents<PooledObject>();
 
        if (poolObj.Length>0)
        {
            foreach (var item in poolObj)
            {
                item.OnSpawn();

            }
            //poolObj.OnSpawn();
        }

        poolDictionary[tag].Enqueue(obj);
        return obj;
    }

    bool  checkObjectActive(GameObject obj)
    {
        var ObjParticle = obj.GetComponent<ParticleSystem>();
        if(ObjParticle)
        {
            return ObjParticle.isPlaying;
        }

        return obj.active;
    }
}


