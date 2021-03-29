using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RescaleTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float size, time;

    bool isResize = false;

    private void OnDisable()
    {
        if (isResize) transform.localScale /= size;
        CancelInvoke();
    }
    void OnEnable()
    {
        isResize = false;
        Invoke("Resize", time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resize()
    {
        transform.localScale *= size;
        isResize = true;
    }
}
