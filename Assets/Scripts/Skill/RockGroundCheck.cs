using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGroundCheck : HitDetection
{
    [SerializeField]
    bool isOnGround;
    void Start()
    {
        OnStart();
        doHitEffect.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }

    public override void OnUpdate()
    {
        ///base.OnUpdate();
        if (!isDetectHit)
        {
            return;
        }

        bool detectGround = false;
        GameObject ground = null;
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position + hitAreaOffset, hitBoxSize, 0);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != null && hitableTag.Contains(hitCollider.tag) && gameObject != hitCollider.gameObject)
            {
                ground = hitCollider.gameObject;

                detectGround = true;

            }

        }
        if(!isOnGround && detectGround)
        {
            Hit(ground);
        }
        isOnGround = detectGround;
    }
}
