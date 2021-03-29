using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFunc : MonoBehaviour
{
    // Start is called before the first frame update
    public float destroyTime;
    float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= startTime + destroyTime) Destroy(gameObject);
    }
}
