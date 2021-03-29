using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearCast : Cast
{
    // Start is called before the first frame update
    bool isActive = true;
    void OnEnable()
    {
        //faceDir = GetComponent<DamageObject>().owner.GetComponent<BaseMove>().faceDirection;
        startSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position += transform.right * speed * Time.deltaTime;
            if (Time.time - startSpawnTime >= destroyedTime && haveDestroyedTime) gameObject.SetActive(false);
        }
    }

    public void Stop()
    {
        isActive = false;
    }

    public void StartCast()
    {
        isActive = true;
    }
}
