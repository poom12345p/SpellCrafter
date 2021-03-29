using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float power, radius, delayedTime;
    public string[] tags;
    public ParticleSystem ps;

    ParticleSystem newPS;
    void OnEnable()
    {
        Invoke("Detonate", delayedTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void Detonate()
    {
        if (newPS == null) newPS = Instantiate(ps);
        newPS.transform.position = transform.position;
        newPS.transform.localScale = new Vector3(radius / 2.5f, radius / 2.5f, radius / 2.5f);
        newPS.Play();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D col in colliders)
        {
            Vector2 explosionDir = col.transform.position - transform.position;
            Rigidbody2D rig = col.GetComponent<Rigidbody2D>();
            foreach (string tag in tags)
                if (col.gameObject.tag == tag) rig.AddForce(explosionDir.normalized * power, ForceMode2D.Impulse);
        }
    }
}
