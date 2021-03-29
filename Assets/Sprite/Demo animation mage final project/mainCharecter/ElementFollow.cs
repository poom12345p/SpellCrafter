using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFollow : MonoBehaviour
{
    public GameObject target;
    public float speed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
}
