using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public  FindNearestTarget fnt;
    [SerializeField]
    bool unTurnChidren;
    //Transform[] children;
    [SerializeField]
    float speed;

    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        GameObject target = fnt.currentHitOjb;

        if (target != null)
        {
            var relativePos = target.transform.position - transform.position;
            var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //transform.rotation = rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed);
            if (unTurnChidren)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var c = transform.GetChild(i);
                    c.transform.rotation = Quaternion.identity;
                }
            }
        }
    }
}
