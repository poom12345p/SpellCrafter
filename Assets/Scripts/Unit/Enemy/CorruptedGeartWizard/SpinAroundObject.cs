using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAroundObject : MonoBehaviour
{
    [SerializeField]
    protected ObjectPooler objectPooler;
    [SerializeField]
    protected int amount;
    [SerializeField]
    protected string objectTag;
    protected GameObject[] objects;
     [SerializeField]
    protected float spinSpeed;
    [HideInInspector]
    public bool isCreating;
    // Start is called before the first frame update
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

    protected virtual IEnumerator SpawnObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i] = objectPooler.SpawnFromPool(objectTag, transform.position, Quaternion.identity);
            Debug.Log(i + "|on");
            yield return new WaitUntil(() => objects[i].transform.localRotation.eulerAngles.z >= (360 / amount));

        }
        isCreating = false;
    }

    public void CreateObjects()
    {
        isCreating = true;
        DisableObjects();
        StopCoroutine("SpawnObjects");
        StartCoroutine("SpawnObjects");
    }

    public void DisableObjects()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i]) objects[i].SetActive(false);
        }

    }
}
