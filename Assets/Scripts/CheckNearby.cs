using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearby : MonoBehaviour
{
    public ParticleSystem ps;
    public Vector3 resize;

    Collider2D[] colliders;
    ParticleSystem newPS;
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Scan", 0f);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.localScale - resize);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(colliders.Length);
    }

    void Scan()
    {
        if (newPS == null) newPS = Instantiate(ps);
        colliders = Physics2D.OverlapBoxAll(transform.position, transform.localScale - resize, 0,8);
        foreach(var c in colliders)
        {
            Debug.Log(c);
        }
        if (colliders.Length > 0)
        {
            gameObject.SetActive(false);
            if (newPS != null) newPS.transform.position = transform.position;
            newPS.Play();
        }
    }
}
