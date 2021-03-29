using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMove : EnemyMove
{
    public float fireInterval;
    float fireCount;
    public ObjectPooler bulletPool;
    bool isFiring;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
        OnStart();
    }
    void Update()
    {
        if(isFiring)
        {
            if(fireCount >= fireInterval)
            {
                fireCount = 0.0f;
                FireBullet();
            }
            else
            {
                if(unit && unit.cold && unit.cold.IsStatusActive())
                {
                    fireCount -= Time.deltaTime/2.0f;
                }
                fireCount += Time.deltaTime;
            }
        }
        OnUpdate();
    }

    public void FireBullet()
    {
       var bullet= bulletPool.SpawnFromPool("Bullet",transform.position,transform.rotation);
     //  var dmgObj = bullet.GetComponent<DamageObject>();
       // dmgObj.SetOwner(gameObject.GetComponent<Unit>());

    }

    public void StartFire()
    {
        if (!isFiring)
        {
            isFiring = true;
            fireCount = 0.0f;
        }
    }

    public void EndFire()
    {
        isFiring = false;
    }
}
