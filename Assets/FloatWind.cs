using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatWind : MonoBehaviour
{
    // Start is called before the first frame update
    public float force = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Debug.Log("STAY");
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            col.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
            Debug.Log("UP");
        }
    }

    /*private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
            col.GetComponent<Rigidbody2D>().AddForce(-transform.up * force * Time.deltaTime);
            Debug.Log("DOWN");
        }
    }*/
}
