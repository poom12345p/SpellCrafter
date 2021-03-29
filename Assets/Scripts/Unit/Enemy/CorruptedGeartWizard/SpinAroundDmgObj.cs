using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundDmgObj :SpinAroundObject
{
    [SerializeField]
    protected Unit owner;
    void Start()
    {
        objects = new GameObject[amount];
    }
   
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] && objects[i].active)
            {
                objects[i].transform.Rotate(0.0f, 0.0f, spinSpeed * Time.deltaTime);
                //Debug.Log(i + "|" + woodenShields[i].transform.rotation.eulerAngles.z);
            }
        }
    }

    protected override IEnumerator SpawnObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i] = objectPooler.SpawnFromPool(objectTag, transform.position, Quaternion.identity);
            var dmgobj = objects[i].GetComponentInChildren<DamageObject>();
            if (dmgobj)
                dmgobj.SetOwner(owner);
            //Debug.Log(i + "|on");
            yield return new WaitUntil(() => objects[i].transform.localRotation.eulerAngles.z >= (360 / amount));

        }
    }

   
}
