using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAreaObject : HitDetection
{
    // Start is called before the first frame update
    void Start()
    {
        base.OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        Checking();
    }

    public void Checking()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, hitBoxSize, 0);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null && hitableTag.Contains(hitCollider.tag) && gameObject != hitCollider.gameObject)
            {
                Hit(hitCollider.gameObject);

            }
        }
        
    }
}
