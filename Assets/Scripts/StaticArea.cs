using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticArea : MonoBehaviour
{
    List<GameObject> objs=new List<GameObject>(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InActiveAll()
    {
        foreach(var obj in objs)
        {
            obj.SetActive(false);
        }
    }

    public void AddObj(GameObject obj)
    {
        objs.Add(obj);
    }
}
